using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.ExternalAuth.Facebook.Infrastructure;

/// <summary>
/// Represents plugin route provider
/// </summary>
public class RouteProvider : IRouteProvider
{
    /// <summary>
    /// Register routes
    /// </summary>
    /// <param name="endpointRouteBuilder">Route builder</param>
    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllerRoute(
            name: "AdminProductEdit",
            pattern: "Admin/Product/list",
            defaults: new { controller = "CustomProductListOneWay", action = "List", area = "Admin" }
        );
    }

    /// <summary>
    /// Gets a priority of route provider
    /// </summary>
    public int Priority => 10000;
}