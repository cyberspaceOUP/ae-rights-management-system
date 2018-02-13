using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ACS.Services.Security;

namespace ACS.Services.Master
{
  public partial class PublishingCompanyService : IPublishingCompanyService 
    {
        #region Fields
        private readonly IRepository<PublishingCompanyMaster> _publishingCompanyRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IRepository<GeographicalMaster> _geoMaster;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>  
        /// 
       public PublishingCompanyService(IRepository<PublishingCompanyMaster> publishingCompanyMaster,IEncryptionService encryptionService,IRepository<GeographicalMaster> geoMaster)
       {
           _publishingCompanyRepository = publishingCompanyMaster;
           this._encryptionService = encryptionService;
           _geoMaster = geoMaster;
        }

      #endregion

     #region Method

       public string DuplicityCheck(PublishingCompanyMaster publishingCompanyMaster)
       {

           var dupes = _publishingCompanyRepository.Table.Where(x => x.CompanyName == publishingCompanyMaster.CompanyName && x.Cityid == publishingCompanyMaster.Cityid
                                                    && x.Deactivate == "N"
                                                     && (publishingCompanyMaster.Id != 0 ? x.Id : 0) != (publishingCompanyMaster.Id != 0 ? publishingCompanyMaster.Id : 1)).FirstOrDefault();
           if (dupes != null)
           {
               return "N";

           }
           else
           {
               return "Y";
           }
       }

       public void InsertPublishingCompany(PublishingCompanyMaster _publishingCompany)
       {
           _publishingCompany.Deactivate = "N";
           _publishingCompany.EnteredBy = 10;
           _publishingCompany.EntryDate = DateTime.Now;
           _publishingCompany.ModifiedBy = null;
           _publishingCompany.ModifiedDate = null;
           _publishingCompany.DeactivateBy = null;
           _publishingCompany.DeactivateDate = null;
           _publishingCompanyRepository.Insert(_publishingCompany);
       }

       public void UpdatePublishingCompany(PublishingCompanyMaster publishingCompany)
       {
           _publishingCompanyRepository.Update(publishingCompany);
       }

       public void DeletePublishingCompany(PublishingCompanyMaster publishingCompany)
       {
           _publishingCompanyRepository.Delete(publishingCompany);
       }

       public PublishingCompanyMaster GetPublishingCompanyById(PublishingCompanyMaster publishingCompany)
       {
           return _publishingCompanyRepository.Table.Where(i => i.Id == publishingCompany.Id).FirstOrDefault();
       }

      public virtual IList<PublishingCompanyMaster> GetAllPublishingCompany()
       {
          
         return  _publishingCompanyRepository.Table.Where(i => i.Deactivate == "N").OrderBy(d => d.CompanyName).ToList();
          
         //var geo = _geoMaster.Table.Where(m => m.Deactivate == "N").OrderBy(m => m.geogName).ToList();
         //return  _publishingCompanyRepository.Table.Where(i => i.Deactivate == "N").OrderBy(d => d.CompanyName).ToList().Join(_geoMaster.Table.Where(m => m.Deactivate == "N").OrderBy(m => m.geogName).ToList(), p => p.CountryId, g => g.Id, ((p, g) => _publishingCompanyRepository));

           

          //IList<PublishingCompanyMaster> publist = _publishingCompanyRepository.Table.Where(i => i.Deactivate == "N").OrderBy(d => d.CompanyName).ToList();
          //IList<GeographicalMaster> geolist = _geoMaster.Table.Where(m => m.Deactivate == "N").OrderBy(m => m.geogName).ToList();
          //var result = publist.Join(geolist, p => p.CountryId, g => g.Id, ((p, g) => new { countryName = g.geogName  })).ToList();



          ////foreach (var v in result)
          ////{
          ////    PublishingCompanyMaster objpub = new PublishingCompanyMaster();
          ////    objpub.CountryId = v.countryName;
          ////    publist.Add();
          ////}
          //return publist;
         
       }
     #endregion

    }
}
