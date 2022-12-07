using System;
using System.Web.Http.Controllers;
using Perigee;
using RPCore.Cache;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Jda.WfmEssApi
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public sealed class DataLevelPermissions : ActionFilterAttribute
  {
    public string[] Permissions { get; set; }
    public bool IsAllowPermission { get; set; }

    public override void OnActionExecuting(HttpActionContext actionContext)
    {
      
    }
  }
}