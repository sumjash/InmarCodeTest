using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Perigee;
using RPCore.Cache;

namespace Jda.WfmEssApi.App_Start
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class Authorize : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }
    }
}