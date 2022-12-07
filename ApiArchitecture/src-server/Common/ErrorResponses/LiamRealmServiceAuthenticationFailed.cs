using Jda.WfmEssApi.ApiExceptionCode;
using System;
using System.Net;
using Perigee;

namespace Jda.WfmEssApi.Common
{
  public class LiamRealmServiceAuthenticationFailed : IErrorResponse
  {
    public Enum SourceErrorCode { get; }

    public HttpStatusCode StatusCode { get; }

    public Enum ErrorCode { get; }

    public string Scenario { get; }

    public LiamRealmServiceAuthenticationFailed()
    {
      StatusCode = HttpStatusCode.Unauthorized;
      SourceErrorCode = PersonaAuthorization.PersonaAuthorizationErrorCode.LiamRealmServiceAuthenticationFailed;
      ErrorCode = UserAuthorizationErrorCodes.LiamRealmServiceAuthenticationFailed;
      Scenario = "LIAM Realm Authentication failed.";
    }
  }
}