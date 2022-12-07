using System.Web;
using System.Web.Http;
using Jda.WfmEssApi.ErrorLogging;

namespace Jda.WfmEssApi
{
  public class WebApiConfiguratorImpl
  {
    public string WebApiPrefix => "api";
    public string WebApiExecutionPath => $"~/{WebApiPrefix}";

    public void RegisterFilters(HttpConfiguration config)
    {
      config.Filters.Add(new WfmEssApiAspNetErrorLoggingFilter());
    }

    public void RegisterFormatters(HttpConfiguration config)
    {
      config.Formatters.Clear();
      config.Formatters.Add(new JsonNetMediaTypeFormatterForEssApi());
    }

    public bool IsWebApiRequest()
    {
      return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiExecutionPath);
    }
  }
}