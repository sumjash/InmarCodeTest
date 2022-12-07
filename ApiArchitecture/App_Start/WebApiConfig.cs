using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Jda.WfmEssApi
{
    public static class WebApiConfig
    {
    /// <summary>
    /// /*      config.Routes.MapHttpRoute(
    ///         name: "DefaultApi",
    ///         routeTemplate: "api/{controller}/{id}",
    ///         defaults: new { id = RouteParameter.Optional }
    ///         );*/
    /// 
    ///       //config.EnableQuerySupport();
    /// </summary>
    /// <param name="config"></param>

    public static void Register(HttpConfiguration config)
    {
      // Web API Attribute routes
      config.MapHttpAttributeRoutes();

      // mapping non-zero param controller constructors
      config.DependencyResolver = ControllerDependencyRegistration.CreateDependencyResolver(config.DependencyResolver);

      // To disable tracing in your application, please comment out or remove the following line of code
      TraceConfig.Register(config);
    }
  }
}
