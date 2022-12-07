using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Perigee;

namespace Jda.WfmEssApi.Common
{
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class ResourceCanNotBeNullAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(HttpActionContext actionContext)
    {
      if (actionContext.ActionArguments.ContainsValue(null))
      {
        //TODO: Probably need to centralize the Response Creation Logic as part of the Protocol 
        var modelIsNotValid = actionContext.ModelState.IsValid == false;
        if (modelIsNotValid)
        {
          var userMessage = "The request is malformed and can not be understood. Please correct the request and try again.";
          var devMessage = "No request entity was found but must be provided.";
          var errorCode = "MalformedRequest.NoRequestEntity"; //TODO: change this to something ready for production.
          var errorContainer = new ApiErrorContainerV2(userMessage, errorCode, devMessage);
          actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorContainer);
        }
      }

    }
  }
}