using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;// [TO BE ENABLED FOR WEB API]

using ACS.Core;
using ACS.Core.Infrastructure;
using ACS.Core.Infrastructure.DependencyRegister;
using ACS.Core.Fakes;
using ACS.Core.Data;
using ACS.Data;
using ACS.Core.Caching;
using ACS.Core.Domain.Master;
using ACS.Web.Framework.Mvc.Routes;
//////using ACS.Services.Tasks;
using ACS.Services.Localization;
using ACS.Services.Configuration;
using ACS.Services.Logging;
using ACS.Services.Common;
using ACS.Services.Master;
using ACS.Core.Configuration;
//using ACS.Services.Authentication;
//using ACS.Services.Contact;
using ACS.Services.Security;
using ACS.Services.Authentication;
//using ACS.Services.Directory;
//using ACS.Services.Staff;
//using ACS.Services.Society;
//using ACS.Services.Messages;
//using ACS.Services.Vehicle;
//using ACS.Services.Asset;
//using ACS.Web.Framework.Themes;

namespace ACS.Web.Framework
{
    public class DependencyRegister : IDependencyRegister
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //HTTP context and other related stuff
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            ////user agent helper
            //builder.RegisterType<UserAgentHelper>().As<IUserAgentHelper>().InstancePerLifetimeScope();


            //controllers
            builder.RegisterApiControllers(typeFinder.GetAssemblies().ToArray()); // [TO BE  CHECKED FOR WEB API] //Added by Rahul Kumar on 25/02/2016 to include DI for WebAPI
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());  

            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();


            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dataSettingsManager.LoadSettings());
                var dataProvider = efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();

                builder.Register<IDbContext>(c => new ACSObjectContext(dataProviderSettings.DataConnectionString)).InstancePerLifetimeScope();
            }
            else
            {
                builder.Register<IDbContext>(c => new ACSObjectContext(dataSettingsManager.LoadSettings().DataConnectionString)).InstancePerLifetimeScope();
            }


            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            ////plugins
            //builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerLifetimeScope();

            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_static").SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_per_request").InstancePerLifetimeScope();


            //work context
             builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            ////store context
            //builder.RegisterType<WebStoreContext>().As<IStoreContext>().InstancePerLifetimeScope();

            ////builder.RegisterType<ContactService>().As<IContactService>().InstancePerLifetimeScope();
            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();
            builder.RegisterType<NavigationService>().As<INavigationService>().InstancePerLifetimeScope();
            /////builder.RegisterType<FlatService>().As<IFlatService>().InstancePerLifetimeScope();

            builder.RegisterType<UserAuthenticationService>().As<IUserAuthenticationService>().InstancePerLifetimeScope();
            //////builder.RegisterType < ACS.Services.User.UserService>().As<ACS.Services.User.IUserService>().InstancePerLifetimeScope();
            //////builder.RegisterType<ACS.Services.Society.TowerService>().As<ACS.Services.Society.ITowerService>().InstancePerLifetimeScope();
            //////builder.RegisterType<ACS.Services.Society.BlockService>().As<ACS.Services.Society.IBlockService>().InstancePerLifetimeScope();
            //////builder.RegisterType<ACS.Services.NoticeBoard.NoticeBoardService>().As<ACS.Services.NoticeBoard.INoticeBoardService>().InstancePerLifetimeScope();
            //////builder.RegisterType<ACS.Services.NoticeBoard.NoticeBoardVisibilityService>().As<ACS.Services.NoticeBoard.INoticeBoardVisibilityService>().InstancePerLifetimeScope();
            //////builder.RegisterType<ACS.Services.NoticeBoard.NotificationService>().As<ACS.Services.NoticeBoard.INotificationService>().InstancePerLifetimeScope();

            ////services
            //builder.RegisterType<BackInStockSubscriptionService>().As<IBackInStockSubscriptionService>().InstancePerLifetimeScope();
            //builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            //builder.RegisterType<CompareProductsService>().As<ICompareProductsService>().InstancePerLifetimeScope();
            //builder.RegisterType<RecentlyViewedProductsService>().As<IRecentlyViewedProductsService>().InstancePerLifetimeScope();
            //builder.RegisterType<ManufacturerService>().As<IManufacturerService>().InstancePerLifetimeScope();
            //builder.RegisterType<PriceFormatter>().As<IPriceFormatter>().InstancePerLifetimeScope();
            //builder.RegisterType<ProductAttributeFormatter>().As<IProductAttributeFormatter>().InstancePerLifetimeScope();
            //builder.RegisterType<ProductAttributeParser>().As<IProductAttributeParser>().InstancePerLifetimeScope();
            //builder.RegisterType<ProductAttributeService>().As<IProductAttributeService>().InstancePerLifetimeScope();
            //builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            //builder.RegisterType<CopyProductService>().As<ICopyProductService>().InstancePerLifetimeScope();
            //builder.RegisterType<SpecificationAttributeService>().As<ISpecificationAttributeService>().InstancePerLifetimeScope();
            //builder.RegisterType<ProductTemplateService>().As<IProductTemplateService>().InstancePerLifetimeScope();
            //builder.RegisterType<CategoryTemplateService>().As<ICategoryTemplateService>().InstancePerLifetimeScope();
            //builder.RegisterType<ManufacturerTemplateService>().As<IManufacturerTemplateService>().InstancePerLifetimeScope();
            ////pass MemoryCacheManager as cacheManager (cache settings between requests)
            //builder.RegisterType<ProductTagService>().As<IProductTagService>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<AffiliateService>().As<IAffiliateService>().InstancePerLifetimeScope();
            //builder.RegisterType<VendorService>().As<IVendorService>().InstancePerLifetimeScope();
            //builder.RegisterType<AddressService>().As<IAddressService>().InstancePerLifetimeScope();
            //builder.RegisterType<SearchTermService>().As<ISearchTermService>().InstancePerLifetimeScope();
           // builder.RegisterType<GenericAttributeService>().As<IGenericAttributeService>().InstancePerLifetimeScope();
            //builder.RegisterType<FulltextService>().As<IFulltextService>().InstancePerLifetimeScope();
            //builder.RegisterType<MaintenanceService>().As<IMaintenanceService>().InstancePerLifetimeScope();


            //builder.RegisterType<CustomerAttributeParser>().As<ICustomerAttributeParser>().InstancePerLifetimeScope();
            //builder.RegisterType<CustomerAttributeService>().As<ICustomerAttributeService>().InstancePerLifetimeScope();
            //builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            //builder.RegisterType<CustomerRegistrationService>().As<ICustomerRegistrationService>().InstancePerLifetimeScope();
            //builder.RegisterType<CustomerReportService>().As<ICustomerReportService>().InstancePerLifetimeScope();

            ////pass MemoryCacheManager as cacheManager (cache settings between requests)
            //builder.RegisterType<PermissionService>().As<IPermissionService>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
            //    .InstancePerLifetimeScope();
            ////pass MemoryCacheManager as cacheManager (cache settings between requests)
            //builder.RegisterType<AclService>().As<IAclService>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
            //    .InstancePerLifetimeScope();
            ////pass MemoryCacheManager as cacheManager (cache settings between requests)
            //builder.RegisterType<PriceCalculationService>().As<IPriceCalculationService>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<GeoLookupService>().As<IGeoLookupService>().InstancePerLifetimeScope();
            
            //builder.RegisterType<CountryService>().As<ICountryService>().InstancePerLifetimeScope();
            //builder.RegisterType<StateService>().As<IStateService>().InstancePerLifetimeScope();
            builder.RegisterType<ACS.Services.Master.DepartmentService>().As<ACS.Services.Master.IDepartmentService>().InstancePerLifetimeScope();
            builder.RegisterType<ACS.Services.Master.CommonListService>().As<ACS.Services.Master.ICommonListService>().InstancePerLifetimeScope();



             builder.RegisterType<ACS.Services.User.UserService>().As<ACS.Services.User.IUserService>().InstancePerLifetimeScope();


             builder.RegisterType<ACS.Services.Contact.ContactService>().As<ACS.Services.Contact.IContactService>().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.Master.DivisionService  >().As<ACS.Services.Master.IDivisionService >().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.Common.CommonDropDown>().As<ACS.Services.Common.ICommonDropDown>().InstancePerLifetimeScope();

            
            builder.RegisterType<ACS.Services.Master.Exeutive>().As<ACS.Services.Master.IExecutive>().InstancePerLifetimeScope();
           //// service add by anjali  
           //// created by 05/13/2016 for ProductTypeService
             builder.RegisterType<ACS.Services.Master.ProductTypeService>().As<ACS.Services.Master.IProductType>().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.Master.PageAccessService>().As<ACS.Services.Master.IPageAccessService>().InstancePerLifetimeScope();
            // added by saddam 17/05/2016
             builder.RegisterType<ACS.Services.Master.AuthorService>().As<ACS.Services.Master.IAuthorService>().InstancePerLifetimeScope();
            //ended saddam

            //Added By sanjeet 18/05/2016
             builder.RegisterType<ACS.Services.Master.PublishingCompanyService>().As<ACS.Services.Master.IPublishingCompanyService>().InstancePerLifetimeScope();
            //


             // added by saddam 25/05/2016
             builder.RegisterType<ACS.Services.Master.CustomProductService>().As<ACS.Services.Master.ICustomProductService>().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.Product.ProductMasterService>().As<ACS.Services.Product.IProductMasterService>().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.Product.ProductLicenseService>().As<ACS.Services.Product.IProductLicenseService>().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.Product.AddendumServices>().As<ACS.Services.Product.IAddendumServices>().InstancePerLifetimeScope();

             //ended saddam

             // added by saddam 30/05/2016
             builder.RegisterType<ACS.Services.Master.ApplicationSetUpService>().As<ACS.Services.Master.IApplicationSetUpService>().InstancePerLifetimeScope();
        
             //ended saddam

             // added by saddam 14/06/2016
             builder.RegisterType<ACS.Services.Other_Contract.OtherContractService>().As<ACS.Services.Other_Contract.IOtherContractService>().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.AuthorContract.AuthorContractService>().As<ACS.Services.AuthorContract.IAuthorContractService>().InstancePerLifetimeScope();
             //ended saddam

             // added by saddam 01/07/2016
             builder.RegisterType<ACS.Services.Master.ISBNService>().As<ACS.Services.Master.IISBNService>().InstancePerLifetimeScope();
              //ended saddam

             // added by saddam 27/07/2016
             builder.RegisterType<ACS.Services.RightsSelling.RightsSelling >().As<ACS.Services.RightsSelling.IRightsSelling>().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.PermissionsOutbound.PermissionsOutboundService>().As<ACS.Services.PermissionsOutbound.IPermissionsOutboundService>().InstancePerLifetimeScope();
            //ended saddam

            //Added by Saddam on 02/08/2016
             builder.RegisterType<ACS.Services.PermissionsInbound.PermissionsInboundService>().As<ACS.Services.PermissionsInbound.IPermissionsInboundService>().InstancePerLifetimeScope();
            //ended by Saddam

             //Added by Suranjana 11/07/2016
             builder.RegisterType<ACS.Services.Master.TypeOfRightsService>().As<ACS.Services.Master.ITypeOfRightsService>().InstancePerLifetimeScope();
             //ended Suranjana

             //Added by Ankush 
            //12/07/2016
             builder.RegisterType<ACS.Services.Master.SubsidiaryRightsService>().As<ACS.Services.Master.ISubsidiaryRightsService>().InstancePerLifetimeScope();

             //13/07/2016
             builder.RegisterType<ACS.Services.Master.GeographicalService>().As<ACS.Services.Master.IGeographicalService>().InstancePerLifetimeScope();
            //14/07/2016
             builder.RegisterType<ACS.Services.Master.LanguageMasterService>().As<ACS.Services.Master.ILanguageMasterService>().InstancePerLifetimeScope();
            //15/07/2016
             builder.RegisterType<ACS.Services.Master.ImprintService>().As<ACS.Services.Master.IImprintService>().InstancePerLifetimeScope();

             //ended Ankush
            
             //Added by Suranjana 13/07/2016
             builder.RegisterType<ACS.Services.Master.TerritoryRightsService>().As<ACS.Services.Master.ITerritoryRightsService>().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.Master.ManuscriptDeliveryFormatService>().As<ACS.Services.Master.IManuscriptDeliveryFormatService>().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.Master.SupplyMaterialService>().As<ACS.Services.Master.ISupplyMaterialService>().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.Master.SeriesService>().As<ACS.Services.Master.ISeriesService>().InstancePerLifetimeScope();
             //ended Suranjana

             //Added by Suranjana 14/07/2016
             builder.RegisterType<ACS.Services.Master.PubCenterService>().As<ACS.Services.Master.IPubCenterService>().InstancePerLifetimeScope();
             //ended Suranjana

             //Added by Suranjana 19/07/2016
             builder.RegisterType<ACS.Services.Master.LicenseeService>().As<ACS.Services.Master.ILicenseeService>().InstancePerLifetimeScope();
             builder.RegisterType<ACS.Services.Master.CopyrightHolderService>().As<ACS.Services.Master.ICopyrightHolderService>().InstancePerLifetimeScope();
             //ended Suranjana

            /*Added by Rajneesh Singh on 01/08/2016*/
             builder.RegisterType<ACS.Services.Product.SeriesProductEntryService>().As<ACS.Services.Product.ISeriesProductEntryService>().InstancePerLifetimeScope();
            /*Ended by Rajneesh Singh*/



             /*Added by Saddam on 27/09/2016*/
             builder.RegisterType<ACS.Services.Alert.ServiceApplicationEmailSetup>().As<ACS.Services.Alert.IServiceApplicationEmailSetup>().InstancePerLifetimeScope();
             /*Ended by Saddam*/

            ////builder.RegisterType<ACS.Services.Directory.GeographyService>().As<ACS.Services.Directory.IGeographyService>().InstancePerLifetimeScope();

            ////builder.RegisterType<ACS.Services.Directory.MasterValueService>().As<ACS.Services.Directory.IMasterValueService>().InstancePerLifetimeScope();

            //////builder.RegisterType<ACS.Services.Contact.VisitorEntryService>().As<ACS.Services.Contact.IVisitorEntryService>().InstancePerLifetimeScope();

            //////builder.RegisterType<TempStaffMasterService>().As<ITempStaffMasterService>().InstancePerLifetimeScope();

            //////builder.RegisterType<StaffMasterService>().As<IStaffMasterService>().InstancePerLifetimeScope();

            //////builder.RegisterType<SocietyService>().As<ISocietyService>().InstancePerLifetimeScope();

            //builder.RegisterType<WorkflowMessageService>().As<IWorkflowMessageService>().InstancePerLifetimeScope();

            //builder.RegisterType<MessageTokenProvider>().As<IMessageTokenProvider>().InstancePerLifetimeScope();

            //builder.RegisterType<MessageTemplateService>().As<IMessageTemplateService>().InstancePerLifetimeScope();

            //builder.RegisterType<QueuedEmailService>().As<IQueuedEmailService>().InstancePerLifetimeScope();

            //builder.RegisterType<Tokenizer>().As<ITokenizer>().InstancePerLifetimeScope();

            //builder.RegisterType<EmailAccountService>().As<IEmailAccountService>().InstancePerLifetimeScope();

            //////builder.RegisterType<VehicleDetailService>().As<IVehicleDetailService>().InstancePerLifetimeScope();
            //////builder.RegisterType<AssetMasterService>().As<IAssetMasterService>().InstancePerLifetimeScope();

            //////builder.RegisterType<AssetAttributeService>().As<IAssetAttributeService>().InstancePerLifetimeScope();
            //////builder.RegisterType<AssetAttributeValueService>().As<IAssetAttributeValueService>().InstancePerLifetimeScope();
            //////builder.RegisterType<SocietyAssetLinkService>().As<ISocietyAssetLinkService>().InstancePerLifetimeScope();
            //////builder.RegisterType<SocietyAssetAttributeValueService>().As<ISocietyAssetAttributeValueService>().InstancePerLifetimeScope();
            //////builder.RegisterType<SocietyAssetImageService>().As<ISocietyAssetImageService>().InstancePerLifetimeScope();
            

            //builder.RegisterType<CurrencyService>().As<ICurrencyService>().InstancePerLifetimeScope();
            //builder.RegisterType<MeasureService>().As<IMeasureService>().InstancePerLifetimeScope();
            //builder.RegisterType<StateProvinceService>().As<IStateProvinceService>().InstancePerLifetimeScope();

            //builder.RegisterType<StoreService>().As<IStoreService>().InstancePerLifetimeScope();
            ////pass MemoryCacheManager as cacheManager (cache settings between requests)
            //builder.RegisterType<StoreMappingService>().As<IStoreMappingService>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<DiscountService>().As<IDiscountService>().InstancePerLifetimeScope();


            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<SettingService>().As<ISettingService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterSource(new SettingsSource());

            ////pass MemoryCacheManager as cacheManager (cache locales between requests)
            builder.RegisterType<LocalizationService>().As<ILocalizationService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();

            //pass MemoryCacheManager as cacheManager (cache locales between requests)
            builder.RegisterType<LocalizedEntityService>().As<ILocalizedEntityService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();

            //builder.RegisterType<DownloadService>().As<IDownloadService>().InstancePerLifetimeScope();
            //builder.RegisterType<PictureService>().As<IPictureService>().InstancePerLifetimeScope();

            //builder.RegisterType<MessageTemplateService>().As<IMessageTemplateService>().InstancePerLifetimeScope();
            //builder.RegisterType<QueuedEmailService>().As<IQueuedEmailService>().InstancePerLifetimeScope();
            //builder.RegisterType<NewsLetterSubscriptionService>().As<INewsLetterSubscriptionService>().InstancePerLifetimeScope();
            //builder.RegisterType<CampaignService>().As<ICampaignService>().InstancePerLifetimeScope();
            //builder.RegisterType<EmailAccountService>().As<IEmailAccountService>().InstancePerLifetimeScope();
            //builder.RegisterType<WorkflowMessageService>().As<IWorkflowMessageService>().InstancePerLifetimeScope();
            //builder.RegisterType<MessageTokenProvider>().As<IMessageTokenProvider>().InstancePerLifetimeScope();
            //builder.RegisterType<Tokenizer>().As<ITokenizer>().InstancePerLifetimeScope();
            //builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();

            //builder.RegisterType<CheckoutAttributeFormatter>().As<ICheckoutAttributeFormatter>().InstancePerLifetimeScope();
            //builder.RegisterType<CheckoutAttributeParser>().As<ICheckoutAttributeParser>().InstancePerLifetimeScope();
            //builder.RegisterType<CheckoutAttributeService>().As<ICheckoutAttributeService>().InstancePerLifetimeScope();
            //builder.RegisterType<GiftCardService>().As<IGiftCardService>().InstancePerLifetimeScope();
            //builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            //builder.RegisterType<OrderReportService>().As<IOrderReportService>().InstancePerLifetimeScope();
            //builder.RegisterType<OrderProcessingService>().As<IOrderProcessingService>().InstancePerLifetimeScope();
            //builder.RegisterType<OrderTotalCalculationService>().As<IOrderTotalCalculationService>().InstancePerLifetimeScope();
            //builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>().InstancePerLifetimeScope();

            //builder.RegisterType<PaymentService>().As<IPaymentService>().InstancePerLifetimeScope();

            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();
            //builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            //builder.RegisterType<FlatAuthenticationService>().As<IFlatAuthenticationService>().InstancePerLifetimeScope();


            ////pass MemoryCacheManager as cacheManager (cache settings between requests)
            //builder.RegisterType<UrlRecordService>().As<IUrlRecordService>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<ShipmentService>().As<IShipmentService>().InstancePerLifetimeScope();
            //builder.RegisterType<ShippingService>().As<IShippingService>().InstancePerLifetimeScope();

            //builder.RegisterType<TaxCategoryService>().As<ITaxCategoryService>().InstancePerLifetimeScope();
            //builder.RegisterType<TaxService>().As<ITaxService>().InstancePerLifetimeScope();
            //builder.RegisterType<TaxCategoryService>().As<ITaxCategoryService>().InstancePerLifetimeScope();

            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();

            ////pass MemoryCacheManager as cacheManager (cache settings between requests)
            //builder.RegisterType<CustomerActivityService>().As<ICustomerActivityService>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
            //    .InstancePerLifetimeScope();

            //if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseFastInstallationService"]) &&
            //    Convert.ToBoolean(ConfigurationManager.AppSettings["UseFastInstallationService"]))
            //{
            //    builder.RegisterType<SqlFileInstallationService>().As<IInstallationService>().InstancePerLifetimeScope();
            //}
            //else
            //{
            //    builder.RegisterType<CodeFirstInstallationService>().As<IInstallationService>().InstancePerLifetimeScope();
            //}

            //builder.RegisterType<ForumService>().As<IForumService>().InstancePerLifetimeScope();

            //builder.RegisterType<PollService>().As<IPollService>().InstancePerLifetimeScope();
            //builder.RegisterType<BlogService>().As<IBlogService>().InstancePerLifetimeScope();
            //builder.RegisterType<WidgetService>().As<IWidgetService>().InstancePerLifetimeScope();
            //builder.RegisterType<TopicService>().As<ITopicService>().InstancePerLifetimeScope();
            //builder.RegisterType<NewsService>().As<INewsService>().InstancePerLifetimeScope();

            //builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerLifetimeScope();
            //builder.RegisterType<SitemapGenerator>().As<ISitemapGenerator>().InstancePerLifetimeScope();
            //builder.RegisterType<PageHeadBuilder>().As<IPageHeadBuilder>().InstancePerLifetimeScope();

           ////// builder.RegisterType<ScheduleTaskService>().As<IScheduleTaskService>().InstancePerLifetimeScope();

            //builder.RegisterType<ExportManager>().As<IExportManager>().InstancePerLifetimeScope();
            //builder.RegisterType<ImportManager>().As<IImportManager>().InstancePerLifetimeScope();
            //builder.RegisterType<PdfService>().As<IPdfService>().InstancePerLifetimeScope();
            //builder.RegisterType<ThemeProvider>().As<IThemeProvider>().InstancePerLifetimeScope();
            //builder.RegisterType<ThemeContext>().As<IThemeContext>().InstancePerLifetimeScope();


            //builder.RegisterType<ExternalAuthorizer>().As<IExternalAuthorizer>().InstancePerLifetimeScope();
            //builder.RegisterType<OpenAuthenticationService>().As<IOpenAuthenticationService>().InstancePerLifetimeScope();


            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();

            ////Register event consumers
            //var consumers = typeFinder.FindClassesOfType(typeof(IConsumer<>)).ToList();
            //foreach (var consumer in consumers)
            //{
            //    builder.RegisterType(consumer)
            //        .As(consumer.FindInterfaces((type, criteria) =>
            //        {
            //            var isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
            //            return isMatch;
            //        }, typeof(IConsumer<>)))
            //        .InstancePerLifetimeScope();
            //}
            //builder.RegisterType<EventPublisher>().As<IEventPublisher>().SingleInstance();
            //builder.RegisterType<SubscriptionService>().As<ISubscriptionService>().SingleInstance();
            
           

        }

        public int Order
        {
            get { return 0; }
        }
    }

    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
                Service service,
                Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
        {
            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    //var currentStoreId = c.Resolve<IStoreContext>().CurrentStore.Id;
                    //uncomment the code below if you want load settings per store only when you have two stores installed.
                    //var currentStoreId = c.Resolve<IStoreService>().GetAllStores().Count > 1
                    //    c.Resolve<IStoreContext>().CurrentStore.Id : 0;

                    //although it's better to connect to your database and execute the following SQL:
                    //DELETE FROM [Setting] WHERE [StoreId] > 0
                    //return c.Resolve<ISettingService>().LoadSetting<TSettings>(currentStoreId);
                    return c.Resolve<ISettingService>().LoadSetting<TSettings>();
                })
                .InstancePerLifetimeScope()
                .CreateRegistration();
        }

        public bool IsAdapterForIndividualComponents { get { return false; } }
    }
}
