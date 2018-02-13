//create by saddam 
//date : 24/05/2016
//purpose : Insert, Update, Delete Records for Custom Product insert data in two table ProprietorMaster, ProprietorAuthorLink
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Product;
namespace ACS.Services.Master
{
   

    public partial interface ICustomProductService
    {

        IList<ProprietorMaster> GetAllCustomproduct();


        string DuplicityCheck(ProprietorMaster custom);
        /// <summary>
        /// insert customproduct 
        /// </summary>
        /// <param name="city">city class object</param>
        /// <returns></returns>
        void insertcustomproduct(ProprietorMaster custom);

        /// <summary>
        /// get customproduct
        /// </summary>
        /// <param name="geography city">customproduct as object</param>
        /// <returns>division</returns>
        ProprietorMaster getcustomproductbyid(ProprietorMaster custom);

        /// <summary>
        /// update customproduct 
        /// </summary>
        /// <param name="division">customproduct class object</param>
        /// <returns></returns>
        void updatecustomproduct(ProprietorMaster custom);


        /// <summary>
        /// delete customproduct 
        /// </summary>
        /// <param name="division">delete customproduct object</param>
        /// <returns></returns>
        void deletecustomproduct(ProprietorMaster custom);

       
    }
}
