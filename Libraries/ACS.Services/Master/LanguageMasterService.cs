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
    /// LanguageMasterService
    /// created By : Ankush Kumar
    /// Date : 11/07/2016
    /// </summary>
    public partial class LanguageMasterService : ILanguageMasterService
    {
        #region Fields
        private readonly IRepository<LanguageMaster> _languageMasterServiceRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="LanguageMaster">LanguageMaster</param>        
        public LanguageMasterService(
            IRepository<LanguageMaster> LanguageServiceRepository
        )
        {
            _languageMasterServiceRepository = LanguageServiceRepository;
        }
        #endregion


            #region Methods

            /// <summary>
            /// Check The duplicity of record before insertion and deletion
            /// </summary>
            /// <param name="GeographyType">Geography</param>        
        public string DuplicityCheck(LanguageMaster Language)
            {

                var dupes = _languageMasterServiceRepository.Table.Where(x => x.LanguageName == Language.LanguageName
                                                                && x.Deactivate == "N"
                                                                && (Language.Id != 0 ? x.Id : 0) != (Language.Id != 0 ? Language.Id : 1)).FirstOrDefault();
                if (dupes != null)
                {
                    return "N";

                }
                else
                {
                    return "Y";
                }
            }

        public void InsertLanguage(LanguageMaster Language)
            {
                Language.Deactivate = "N";
                Language.LanguageCode = Language.LanguageName.Replace(".", string.Empty).Substring(0, 3).ToUpper();
                Language.EntryDate = DateTime.Now;
                Language.ModifiedBy = null;
                Language.ModifiedDate = null;
                Language.DeactivateBy = null;
                Language.DeactivateDate = null;
                _languageMasterServiceRepository.Insert(Language);
            }

        public LanguageMaster GetLanguageById(int Id)
            {
                return _languageMasterServiceRepository.Table.Where(i => i.Id == Id).FirstOrDefault();
            }

        public void UpdateLanguage(LanguageMaster Language)
            {
                _languageMasterServiceRepository.Update(Language);
            }

        public void DeleteSubsidiaryRights(LanguageMaster Language)
            {
                _languageMasterServiceRepository.Delete(Language);
            }


        public IList<LanguageMaster> GetAllLanguage()
            {
                var query = _languageMasterServiceRepository.Table;
                var Language = query.Where(d => d.LanguageName != null && d.Deactivate == "N").OrderBy(c => c.LanguageName)
                    .ToList();

                return Language;

            }

            #endregion

    }
}
