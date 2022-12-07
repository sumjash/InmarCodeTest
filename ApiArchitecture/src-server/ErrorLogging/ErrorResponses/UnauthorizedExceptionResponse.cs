using System;
using System.Net;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.ErrorLogging.ErrorResponses
{
  public enum Security
  {
    NotAuthorized
  }

  public class UnauthorizedExceptionResponse : IErrorResponse
  {
    public Enum SourceErrorCode { get; }
    public HttpStatusCode StatusCode { get; }
    public Enum ErrorCode { get; }
    public string Scenario { get; }

    public UnauthorizedExceptionResponse()
    {
      StatusCode = HttpStatusCode.Forbidden;
      ErrorCode = Security.NotAuthorized;
      Scenario = "User is unauthorized.";
    }
  }
}