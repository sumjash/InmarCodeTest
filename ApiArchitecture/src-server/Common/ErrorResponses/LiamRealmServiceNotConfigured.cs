using Jda.WfmEssApi.ApiExceptionCode;
using System;
using System.Net;
using Perigee;

namespace Jda.WfmEssApi.Common
{
  public class LiamRealmServiceNotConfigured : IErrorResponse
  {
    public Enum SourceErrorCode { get; }

    public HttpStatusCode StatusCode { get; }

    public Enum ErrorCode { get; }

    public string Scenario { get; }

    public LiamRealmServiceNotConfigured()
    {
      StatusCode = HttpStatusCode.NotFound;
      SourceErrorCode = PersonaAuthorization.PersonaAuthorizationErrorCode.LiamRealmServiceNotConfigured;
      ErrorCode = UserAuthorizationErrorCodes.LiamRealmServiceNotConfigured;
      Scenario = "LIAM Realm Service is not configured";
    }
  }
}