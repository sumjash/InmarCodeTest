using Jda.WfmEssApi.ApiExceptionCode;
using System;
using System.Net;
using Perigee;

namespace Jda.WfmEssApi.Common
{
  public class UnauthorizedAccess : IErrorResponse
  {
    public Enum SourceErrorCode { get; }

    public HttpStatusCode StatusCode { get; }

    public Enum ErrorCode { get; }

    public string Scenario { get; }

    public UnauthorizedAccess()
    {
      StatusCode = HttpStatusCode.Forbidden;
      SourceErrorCode = PersonaAuthorization.PersonaAuthorizationErrorCode.UnauthorizedAccess;
      ErrorCode = UserAuthorizationErrorCodes.UnauthorizedAccess;
      Scenario = "User is not authorized to access this resource";
    }
  }
}