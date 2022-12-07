using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Perigee;

namespace Jda.WfmEssApi.Common
{
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class RequiresModelValidationAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(HttpActionContext actionContext)
    {
      //TODO: Probably need to centralize the Response Creation Logic as part of the Protocol 
      var modelIsNotValid = actionContext.ModelState.IsValid == false;
      if (modelIsNotValid)
      {
        var userMessage = "The request is malformed and can not be understood. Please correct the request and try again.";
        var errorCode = "MalformedRequest.InvalidModelState"; //TODO: change this to something ready for production.
        var errorContainer = new ApiErrorContainerV2(userMessage, errorCode, actionContext.ModelState);
        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorContainer);
      }
    }

  }
}