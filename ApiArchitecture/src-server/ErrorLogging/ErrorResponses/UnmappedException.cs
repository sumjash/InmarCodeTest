using System;
using System.Net;
using Jda.WfmEssApi.Common;
using Perigee.GlobalErrorHandling;

namespace Jda.WfmEssApi.ErrorLogging.ErrorResponses
{
  public enum ApiUnmappedErrorCodes
  {
    Unmapped
  }

  public class UnmappedException : IErrorResponse
  {
    public Enum SourceErrorCode { get; }
    public HttpStatusCode StatusCode { get; }
    public Enum ErrorCode { get; }
    public string Scenario { get; }

    public UnmappedException()
    {
      StatusCode = HttpStatusCode.InternalServerError;
      ErrorCode = ApiUnmappedErrorCodes.Unmapped;
      Scenario = OopsResponse.Message;
    }
  }
}