using Perigee.GlobalErrorHandling;
using Jda.WfmEssApi.Common.Enums;
using System;
using System.Web.Http.Routing;

namespace Jda.WfmEssApi.Common
{
  public static class ResourceHelper
  {
    private static UrlHelper _UrlHelper;

    public static UrlHelper UrlHelper
    {
      get
      {
        return _UrlHelper;

      }
      set
      {
        if (_UrlHelper == null)
        {
          _UrlHelper = value;
        }
      }
    }

    public static string GenerateUri(string routeName)
    {
      return GenerateUri(routeName, new { });
    }

    public static string GenerateUri(string routeName, object routeValues)
    {
      try
      {
        var fullVirtualUrlRoute = UrlHelper.Route(routeName, routeValues);
        return RemoveApplicationVirtualPath(fullVirtualUrlRoute);
      }
      catch (NullReferenceException)
      {
        var weGotHereBecauseOfHelpDocsRequestSoGiveSampleText = "some url";
        return weGotHereBecauseOfHelpDocsRequestSoGiveSampleText;
      }
      catch (Exception)
      {
        throw new CustomApplicationException<ResourceHelperErrorCodes>(ResourceHelperErrorCodes.ErrorInUriGeneration);
      }
    }

    internal static string RemoveApplicationVirtualPath(string url)
    {
      var sanitizedUrl = url;
      var applicationVirtualPath = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
      var applicationVirtualPathIsNotNull = applicationVirtualPath != null;
      sanitizedUrl = applicationVirtualPathIsNotNull ? sanitizedUrl?.Replace(applicationVirtualPath, "") : sanitizedUrl;
      return sanitizedUrl;
    }
  }
}