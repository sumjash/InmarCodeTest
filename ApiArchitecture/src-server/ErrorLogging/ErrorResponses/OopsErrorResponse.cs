using System;
using System.Net;
using Jda.WfmEssApi.Common;
using Perigee.GlobalErrorHandling;

namespace Jda.WfmEssApi.ErrorLogging.ErrorResponses
{
  public enum General
  {
    Issue
  }

  public class OopsErrorResponse : IErrorResponse
  {
    public Enum SourceErrorCode { get; }
    public HttpStatusCode StatusCode { get; }
    public Enum ErrorCode { get; }
    public string Scenario { get; }

    public OopsErrorResponse()
    {
      StatusCode = HttpStatusCode.InternalServerError;
      ErrorCode = General.Issue;
      Scenario = OopsResponse.Message;
    }
  }
}