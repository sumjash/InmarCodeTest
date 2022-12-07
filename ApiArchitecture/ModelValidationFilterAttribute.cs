using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Perigee;

namespace Jda.WfmEssApi
{
  public class ModelValidationFilterAttribute : ActionFilterAttribute
  {
    protected const string ModelValidationUserMessage = "Model Validation Errors have been found. Please correct.";
    protected const string ModelValidationErrorCode = "ModelValidation.ValidationError";

    public override void OnActionExecuting(HttpActionContext actionContext)
    {
      var noModelStateErrors = actionContext.ModelState.IsValid;
      if (noModelStateErrors)
      {
        return;
      }

      var errors = new Dictionary<string, IEnumerable<string>>();

      foreach (var keyValue in actionContext.ModelState)
      {
        errors[keyValue.Key] = keyValue.Value.Errors.Select(e => e.ErrorMessage);
      }

      var container = new ApiErrorContainerV2(ModelValidationUserMessage, ModelValidationErrorCode, errors);

      actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, container);
    }
  }
}
