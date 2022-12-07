using System;
using System.Collections.Generic;
using System.Linq;

namespace Jda.WfmEssApi.Common
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class ErrorResponsesAttribute : Attribute
  {
    protected readonly Type[] ErrorResponseTypes;

    public IEnumerable<IErrorResponse> ErrorResponses
    {
      get
      {
        return ErrorResponseTypes
          .Select(errorResponseType =>
          {
            var instanceOfErrorResponse = Activator.CreateInstance(errorResponseType) as IErrorResponse;
            var classDoesNotImplementCorrectInterface = instanceOfErrorResponse == null;
            if (classDoesNotImplementCorrectInterface)
            {
              throw new ArgumentException("ErrorResponsesAttribute arguments must implement IErrorResponse");
            }

            return instanceOfErrorResponse;
          });
      }
    }

    public ErrorResponsesAttribute(params Type[] errorResponseTypes)
    {
      ErrorResponseTypes = errorResponseTypes;
    }
  }
}