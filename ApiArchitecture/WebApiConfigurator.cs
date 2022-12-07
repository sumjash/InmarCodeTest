using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace Jda.WfmEssApi
{
  public static class WebApiConfigurator
  {
    public static void Configure(HttpConfiguration config)
    {
      var configurator = new WebApiConfiguratorImpl();
      Configure(config, configurator);
    }

    public static void Configure(HttpConfiguration config, WebApiConfiguratorImpl configurator)
    {
      configurator.RegisterFormatters(config);
      configurator.RegisterFilters(config);
    }

    public static void TellAspToCallCustomSessionStore()
    {
      var configurator = new WebApiConfiguratorImpl();
      TellAspToCallCustomSessionStore(configurator);
    }

    public static void TellAspToCallCustomSessionStore(WebApiConfiguratorImpl configurator)
    {
      if (configurator.IsWebApiRequest())
      {
        HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
      }
    }
  }
}