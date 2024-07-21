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


namespace Nop.Plugin.Misc.WebApi.Frontend.Controllers;

[AutoValidateAntiforgeryToken]
[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class CustomProductListSecondWayController : BasePluginController
{
    #region Fields

    protected readonly IPermissionService _permissionService;
    
    #endregion

    #region Ctor 

    public CustomProductListSecondWayController(IPermissionService permissionService)
        
    {
        _permissionService = permissionService;
        
    }

    #endregion

    #region Methods

    

    public virtual async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
            return AccessDeniedView();

        return View("~/Plugins/Misc.WebApi.Frontend/Views/Configure.cshtml");
    }

    #endregion
}