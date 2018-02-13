using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACS.Core.Domain.Master;
using ACS.Services.Logging;
using ACS.Services.Localization;
using ACS.Services.Master;
using ACS.Core;
using SLV.Model.Common;
using Autofac.Integration.WebApi;
using SLV.API.Controllers.JsonSerializer;
using ACS.Core.Infrastructure;
using ACS.Core.Data;
using System.Web;
using System.IO;

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Xml;
using System.Data.OleDb;

using ACS.Core.Domain.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ACS.Web.Framework.Controllers;
using ACS.Services.Contact;
using ACS.Services.Authentication;
using SLV.Model.Common;
using ACS.Core;
using ACS.Services.Security;
using System.Web.Helpers;
using System.IO;
using ACS.Data;
using ACS.Services.User;


using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Text.RegularExpressions;
using Logger;

namespace SLV.API.Controllers.Master
{
    public class ISBNMasterController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IISBNService _IISBNService;
        private readonly ILocalizationService _localizationService;
        //private readonly ILogger _loggerService;
        private readonly IRepository<Upload_ISBN_Back> _Upload_ISBN_Back;
        private readonly IDbContext _dbContext;
        private readonly IRepository<ApplicationSetUp> _ApplicationSetUp;
        public ISBNMasterController(

               IISBNService IISBNService
          , ILocalizationService localizationService
          // , ILogger loggerService
               , IRepository<Upload_ISBN_Back> Upload_ISBN_Back
              , IDbContext dbContext
           , IRepository<ApplicationSetUp> ApplicationSetUp
            )
        {
            _IISBNService = IISBNService;
            _localizationService = localizationService;
            _Upload_ISBN_Back = Upload_ISBN_Back;
           // _loggerService = loggerService;
            _ApplicationSetUp = ApplicationSetUp;
            this._dbContext = dbContext;

        }

        private DataTable RemoveDuplicatesRecords(DataTable dt)
        {
            //Returns just 5 unique rows
            var UniqueRows = dt.AsEnumerable().Distinct(DataRowComparer.Default);
            DataTable dt2 = UniqueRows.CopyToDataTable();
            return dt2;
        }

        [HttpPost]
        public IHttpActionResult UploadISBN(ISBNBagModel _UploadISBN)
        {
            try
            {

                DataTable InvalidIsbn = new DataTable();
                InvalidIsbn.Columns.Add("Invalid Isbn");
                InvalidIsbn.Columns.Add("Reason");
                IList<ApplicationSetUp> _ApplicationSetUpList = _ApplicationSetUp.Table.Where(x => x.key == "ExcelFilePath" && x.Deactivate == "N").ToList();
                var ExcelPath = _ApplicationSetUpList.Select(Au => new
                {
                    ExcelUploadPath = Au.keyValue,

                });
                string status = "";
                string URL = ExcelPath.FirstOrDefault().ExcelUploadPath;
                int mint_SheetNameFlag = 0;
                DataTable mdt_ISBNBag = new DataTable();
                DataTable mdt_Stock = new DataTable();
                DataTable mdt_AccessCode = new DataTable();
                mdt_Stock = CreateDataTableFromExcelDAL(URL, _UploadISBN.FileName, mint_SheetNameFlag);


                if (mdt_Stock == null)
                {
                    status = "Excel sheet(Sheet1) does not contain isbn's no.";
                    return Json(new { status, InvalidIsbn });
                }

                if (((mdt_Stock != null) && mdt_Stock.Rows.Count < 1))
                {
                    status = "Excel sheet(Sheet1) does not contain isbn's no.";
                    return Json(new { status, InvalidIsbn });
                }


                if (((mdt_Stock != null)))
                {
                    mdt_Stock.TableName = "ISBNBag";
                    if ((mdt_Stock.Columns.Count > 1))
                    {
                        if (((mdt_Stock.Columns[0].ColumnName.ToLower() != "isbn")))
                        {
                            // lit_retval.Text = "Incorrect excel sheet format. Please download correct file format."
                            status = "Invalid excel sheet format. Please download correct file format.";
                            return Json(new { status, InvalidIsbn });
                        }
                    }
                    var duplicates = mdt_Stock.AsEnumerable().GroupBy(r => r[0]).Where(gr => gr.Count() > 1);

                    foreach (var Row in duplicates)
                    {
                        var row = InvalidIsbn.NewRow();
                        row["Invalid Isbn"] = Row.Key;
                        row["Reason"] = "Duplicate isbn found in excel";
                        InvalidIsbn.Rows.Add(row);
                    }
                    DataTable real_datatable = new DataTable();
                    real_datatable = RemoveDuplicatesRecords(mdt_Stock);
                    Regex regex = new Regex(@"^\d$");
                    int flag = 0;
                    IList<ISBNBag> _IListIsbn = new List<ISBNBag>();
                    foreach (DataRow Row in real_datatable.Rows)
                    {
                        ISBNBag _ISBNBag = new ISBNBag();

                        try
                        {
                            if (Row[0].ToString() != "" && System.Text.RegularExpressions.Regex.IsMatch(Decimal.Parse(Row[0].ToString(), System.Globalization.NumberStyles.Any).ToString(), "^[0-9]*$") && Decimal.Parse(Row[0].ToString(), System.Globalization.NumberStyles.Any).ToString().Trim().Length == 13)
                            {
                                _ISBNBag.ISBN = Decimal.Parse(Row[0].ToString(), System.Globalization.NumberStyles.Any).ToString();
                                _ISBNBag.ProductTypeid = _UploadISBN.ProductType;
                                _ISBNBag.Used = "N";
                                _ISBNBag.Deactivate = "N";
                                _ISBNBag.EnteredBy = _UploadISBN.EnteredBy;
                                _ISBNBag.EntryDate = DateTime.Now;
                                _ISBNBag.ProductId = null;

                                int id = _IISBNService.GetIsbnByIsbn(_ISBNBag.ISBN);
                                if (id == 0)
                                {
                                    _IListIsbn.Add(_ISBNBag);
                                    flag = 1;
                                }
                                else
                                {
                                    var row = InvalidIsbn.NewRow();
                                    row["Invalid Isbn"] = Decimal.Parse(Row[0].ToString(), System.Globalization.NumberStyles.Any).ToString();
                                    row["Reason"] = "Isbn already exists in database";
                                    InvalidIsbn.Rows.Add(row);
                                }


                            }
                            else
                            {
                                flag = 2;
                                var row = InvalidIsbn.NewRow();
                                row["Invalid Isbn"] = Decimal.Parse(Row[0].ToString(), System.Globalization.NumberStyles.Any).ToString();
                                row["Reason"] = "Invalid Isbn";
                                InvalidIsbn.Rows.Add(row);
                            }
                        }
                        catch (Exception ex)
                        {
                            var row = InvalidIsbn.NewRow();
                            row["Invalid Isbn"] = Row[0].ToString();
                            row["Reason"] = ex.Message;
                            status = ex.Message;
                            InvalidIsbn.Rows.Add(row);
                        }

                    }

                    if (_IListIsbn.Count > 0)
                    {

                        _IISBNService.InsertISBNBag(_IListIsbn);
                        status = "OK";
                        //return Json(status);
                    }
                    if (InvalidIsbn.Rows.Count > 0)
                    {
                        if (flag == 1)
                        {
                            status = status + ",NOK";
                        }
                        else if (flag == 2)
                        {
                            status = "No isbn have uploaded. All isbn are duplicates.";
                        }


                    }



                }
                return Json(new { status, InvalidIsbn });
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ISBNMasterController.cs", "UploadISBN", ex);
                return Json(new { ex.InnerException.Message });
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ISBNMasterController.cs", "UploadISBN", ex);
                return Json(new { ex.InnerException.Message });
            }
        }

        public IHttpActionResult ISBNSerch(ISBNBagModel OtherContract)
        {

            var _GetOtherContractSearch = _dbContext.ExecuteStoredProcedureListNewData<ISBNBagModel>("Proc_ISBNBag_get").ToList();


            return Json(_GetOtherContractSearch);

        }


        public DataTable CreateDataTableFromExcelDAL(string mstr_path, string mstr_file, int mint_SheetNameFlag)
        {
            

                OleDbConnection mobj_Conn = new OleDbConnection();
                OleDbDataAdapter mobj_dtAdapter = default(OleDbDataAdapter);
                DataTable mobj_dt = new DataTable();
                DataTable mobj_dtSchema = new DataTable();
                DataSet mobj_ds = new DataSet();
                string str = null;
                str = mstr_path + mstr_file;

                try
                {

                    if ((mstr_file.Length > 4))
                    {
                        if ((mstr_file.Substring(mstr_file.Length - 4, 4) == ".xls"))
                        {
                            mobj_Conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + mstr_path + "/" + mstr_file + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'");

                            //  mobj_Conn = new OleDbConnection("Provider= Microsoft.ACE.OLEDB.12.0;Data Source=" + mstr_path + mstr_file + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=2\\'");

                        }
                    }
                    if ((mstr_file.Length > 5))
                    {
                        if ((mstr_file.Substring(mstr_file.Length - 5, 5) == ".xlsx"))
                        {
                            //mobj_Conn = New OleDbConnection("Provider= Microsoft.ACE.OLEDB.12.0;Data Source=" + mstr_path + mstr_file + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1\'")
                            mobj_Conn = new OleDbConnection("Provider= Microsoft.ACE.OLEDB.12.0;Data Source=" + mstr_path + "/" + mstr_file + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1\\'");


                        }
                    }
                    mobj_Conn.Open();


                    mobj_dtSchema = mobj_Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if ((mobj_dtSchema == null))
                    {
                        return null;
                    }

                    string[] excelSheets = new string[mobj_dtSchema.Rows.Count + 1];
                    int i = 0;

                    //' Add the sheet name to the string array.

                    foreach (DataRow row in mobj_dtSchema.Rows)
                    {
                        excelSheets[i] = row["TABLE_NAME"].ToString();
                        if (row["TABLE_NAME"].ToString() == "Sheet1$")
                        {
                            mint_SheetNameFlag = 1;
                        }
                        i = i + 1;
                    }

                    if (mint_SheetNameFlag == 0)
                    {
                        return null;
                    }

                    mobj_dtAdapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", mobj_Conn);
                    mobj_dtAdapter.Fill(mobj_ds, mstr_file);
                    mobj_dt = mobj_ds.Tables[0];
                    mobj_dtAdapter.Dispose();
                    mobj_Conn.Close();
                    mobj_Conn.Dispose();
                }
                catch (ACSException ex)
                {
                    _ILog.LogException("", Severity.ProcessingError, "ISBNMasterController.cs", "UploadISBN", ex);
                }
                catch (Exception ex)
                {
                    _ILog.LogException("", Severity.ProcessingError, "ISBNMasterController.cs", "UploadISBN", ex);
                }
            

                return mobj_dt;
                //*** Return DataTable ***'
        }


    }
}