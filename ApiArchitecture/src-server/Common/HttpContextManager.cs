using System;
using System.Web;

namespace Jda.WfmEssApi.Common
{
  public static class HttpContextManager
  {
    private static HttpContextBase _Context;
    public static HttpContextBase Current {
      get {
        if (_Context != null)
          return _Context;

        if (HttpContext.Current == null)
          throw new InvalidOperationException("HttpContext not available");

        return new HttpContextWrapper(HttpContext.Current);
      }
    }

    public static void SetCurrentContext(HttpContextBase context)
    {
      _Context = context;
    }
  }
}