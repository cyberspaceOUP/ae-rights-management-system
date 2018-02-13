using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Data;
using ACS.Core.Domain.Master;

namespace ACS.Services.Master
{
    /// <summary>
    /// CountryService
    /// created By : Ankush Kumar
    /// Date : 13/07/2016
    /// </summary>
    public partial class GeographicalService : IGeographicalService
    {
        #region Fields
        private readonly IRepository<GeographicalMaster> _geographicalServiceRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public GeographicalService(
            IRepository<GeographicalMaster> CountryServiceRepository
        )
        {
            _geographicalServiceRepository = CountryServiceRepository;
        }
        #endregion


        #region Methods

        /// <summary>
        /// Check The duplicity of record before insertion and deletion
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public string DuplicityCheck(GeographicalMaster Geographical)
        {

            var dupes = _geographicalServiceRepository.Table.Where(x => x.geogName == Geographical.geogName
                                                            && x.Deactivate == "N"
                                                            && x.geogtype == Geographical.geogtype
                                                            && x.parentid == Geographical.parentid
                                                            && (Geographical.Id != 0 ? x.Id : 0) != (Geographical.Id != 0 ? Geographical.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                return "N";

            }
            else
            {
                return "Y";
            }
        }

        public void InsertGeographical(GeographicalMaster Geographical)
        {
            Geographical.Deactivate = "N";
            Geographical.parentid = Geographical.parentid;
            Geographical.geogcode = Geographical.geogName.Replace(".", string.Empty).Substring(0, 2).ToUpper();
            Geographical.EntryDate = DateTime.Now;
            Geographical.ModifiedBy = null;
            Geographical.ModifiedDate = null;
            Geographical.DeactivateBy = null;
            Geographical.DeactivateDate = null;
            _geographicalServiceRepository.Insert(Geographical);
        }

        public GeographicalMaster GetGeographicalById(int Id)
        {
            return _geographicalServiceRepository.Table.Where(i => i.Id == Id).FirstOrDefault();
        }

        public void UpdateGeographical(GeographicalMaster Geographical)
        {
            _geographicalServiceRepository.Update(Geographical);
        }

        public void DeleteGeographical(GeographicalMaster Geographical)
        {
            _geographicalServiceRepository.Delete(Geographical);
        }

        public IList<GeographicalMaster> GetGeographicalList()
        {
            var query = _geographicalServiceRepository.Table;
            var Departments = query.Where(d => d.geogName != null && d.Deactivate == "N").OrderBy(c => c.geogName)
                .ToList();

            return Departments;

        }

        public IList<GeographicalMaster> GetGeographicalList(string geogtype, int? parentid=null)
        {
            var query = _geographicalServiceRepository.Table;
            if (geogtype=="Country")
            {
                var Departments = query.Where(d => d.geogName != null && d.Deactivate == "N" && d.geogtype == geogtype).OrderBy(c => c.geogName)
                    .ToList();

                return Departments;
            }
            else if (parentid == null)
            {
                var Departments = query.Where(d => d.geogName != null && d.Deactivate == "N" && d.geogtype == geogtype).OrderBy(c => c.geogName)
                    .ToList();

                return Departments;
            }
            else
            {
                var Departments = query.Where(d => d.geogName != null && d.Deactivate == "N" && d.geogtype == geogtype && d.parentid == parentid).OrderBy(c => c.geogName)
                    .ToList();

                return Departments;
            }
        }

        #endregion

    }
}
