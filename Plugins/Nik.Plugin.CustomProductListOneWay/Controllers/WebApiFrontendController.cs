using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Catalog;

namespace Nop.Plugin.Misc.WebApi.Frontend.Controllers;

[AutoValidateAntiforgeryToken]
[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class CustomProductListOneWayController : Nop.Web.Areas.Admin.Controllers.ProductController
{
    #region Fields

    //protected readonly IPermissionService _permissionService;
    protected readonly IAclService _aclService;
    protected readonly IBackInStockSubscriptionService _backInStockSubscriptionService;
    protected readonly ICategoryService _categoryService;
    protected readonly ICopyProductService _copyProductService;
    protected readonly ICustomerActivityService _customerActivityService;
    protected readonly ICustomerService _customerService;
    protected readonly IDiscountService _discountService;
    protected readonly IDownloadService _downloadService;
    protected readonly IExportManager _exportManager;
    protected readonly IGenericAttributeService _genericAttributeService;
    protected readonly IHttpClientFactory _httpClientFactory;
    protected readonly IImportManager _importManager;
    protected readonly ILanguageService _languageService;
    protected readonly ILocalizationService _localizationService;
    protected readonly ILocalizedEntityService _localizedEntityService;
    protected readonly IManufacturerService _manufacturerService;
    protected readonly INopFileProvider _fileProvider;
    protected readonly INotificationService _notificationService;
    protected readonly IPdfService _pdfService;
    protected readonly IPermissionService _permissionService;
    protected readonly IPictureService _pictureService;
    protected readonly IProductAttributeFormatter _productAttributeFormatter;
    protected readonly IProductAttributeParser _productAttributeParser;
    protected readonly IProductAttributeService _productAttributeService;
    protected readonly IProductModelFactory _productModelFactory;
    protected readonly IProductService _productService;
    protected readonly IProductTagService _productTagService;
    protected readonly ISettingService _settingService;
    protected readonly IShippingService _shippingService;
    protected readonly IShoppingCartService _shoppingCartService;
    protected readonly ISpecificationAttributeService _specificationAttributeService;
    protected readonly IStoreContext _storeContext;
    protected readonly IUrlRecordService _urlRecordService;
    protected readonly IVideoService _videoService;
    protected readonly IWebHelper _webHelper;
    protected readonly IWorkContext _workContext;
    protected readonly VendorSettings _vendorSettings;
    private static readonly char[] _separator = [','];

    #endregion

    #region Ctor 

    public CustomProductListOneWayController(//IPermissionService permissionService,
        IAclService aclService,
        IBackInStockSubscriptionService backInStockSubscriptionService,
        ICategoryService categoryService,
        ICopyProductService copyProductService,
        ICustomerActivityService customerActivityService,
        ICustomerService customerService,
        IDiscountService discountService,
        IDownloadService downloadService,
        IExportManager exportManager,
        IGenericAttributeService genericAttributeService,
        IHttpClientFactory httpClientFactory,
        IImportManager importManager,
        ILanguageService languageService,
        ILocalizationService localizationService,
        ILocalizedEntityService localizedEntityService,
        IManufacturerService manufacturerService,
        INopFileProvider fileProvider,
        INotificationService notificationService,
        IPdfService pdfService,
        IPermissionService permissionService,
        IPictureService pictureService,
        IProductAttributeFormatter productAttributeFormatter,
        IProductAttributeParser productAttributeParser,
        IProductAttributeService productAttributeService,
        IProductModelFactory productModelFactory,
        IProductService productService,
        IProductTagService productTagService,
        ISettingService settingService,
        IShippingService shippingService,
        IShoppingCartService shoppingCartService,
        ISpecificationAttributeService specificationAttributeService,
        IStoreContext storeContext,
        IUrlRecordService urlRecordService,
        IVideoService videoService,
        IWebHelper webHelper,
        IWorkContext workContext,
        VendorSettings vendorSettings) :base(aclService, backInStockSubscriptionService, categoryService, copyProductService, customerActivityService, customerService, discountService,
downloadService,
 exportManager,
 genericAttributeService,
 httpClientFactory,
 importManager,
languageService,
localizationService,
 localizedEntityService,
 manufacturerService,
 fileProvider,
 notificationService,
pdfService,
 permissionService,
 pictureService,
productAttributeFormatter,
 productAttributeParser,
 productAttributeService,
 productModelFactory,
 productService,
 productTagService,
 settingService,
shippingService,
 shoppingCartService,
specificationAttributeService,
 storeContext,
 urlRecordService,
 videoService,
 webHelper,
 workContext,
 vendorSettings)
    {
        //_permissionService = permissionService;
        _aclService = aclService;
        _backInStockSubscriptionService = backInStockSubscriptionService;
        _categoryService = categoryService;
        _copyProductService = copyProductService;
        _customerActivityService = customerActivityService;
        _customerService = customerService;
        _discountService = discountService;
        _downloadService = downloadService;
        _exportManager = exportManager;
        _genericAttributeService = genericAttributeService;
        _httpClientFactory = httpClientFactory;
        _importManager = importManager;
        _languageService = languageService;
        _localizationService = localizationService;
        _localizedEntityService = localizedEntityService;
        _manufacturerService = manufacturerService;
        _fileProvider = fileProvider;
        _notificationService = notificationService;
        _pdfService = pdfService;
        _permissionService = permissionService;
        _pictureService = pictureService;
        _productAttributeFormatter = productAttributeFormatter;
        _productAttributeParser = productAttributeParser;
        _productAttributeService = productAttributeService;
        _productModelFactory = productModelFactory;
        _productService = productService;
        _productTagService = productTagService;
        _settingService = settingService;
        _shippingService = shippingService;
        _shoppingCartService = shoppingCartService;
        _specificationAttributeService = specificationAttributeService;
        _storeContext = storeContext;
        _urlRecordService = urlRecordService;
        _videoService = videoService;
        _webHelper = webHelper;
        _workContext = workContext;
        _vendorSettings = vendorSettings;
    }

    #endregion

    #region Methods

    public override async Task<IActionResult> List()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
            return AccessDeniedView();

        //prepare model
        var model = await _productModelFactory.PrepareProductSearchModelAsync(new ProductSearchModel());

        return View(model);
        //return View("~/Plugins/Misc.WebApi.Frontend/Views/List.cshtml",model);
    }

    #endregion
}