using System;
using System.Web.Http.Filters;
using Perigee;
using RPCore.Cache;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Jda.WfmEssApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class PermissionsAttribute : ActionFilterAttribute
    {
        public string Permission { get; set; }
        public bool IsAllowPermission { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            
        }
    }
}