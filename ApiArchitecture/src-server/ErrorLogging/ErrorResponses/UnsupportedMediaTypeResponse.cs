using Jda.WfmEssApi.Common;
using System;
using System.Net;

namespace Jda.WfmEssApi.ErrorLogging.ErrorResponses
{
  public enum JsonReader
  {
    DeserializationFailed
  }

  public class UnsupportedMediaTypeResponse : IErrorResponse
  {
    public Enum SourceErrorCode { get; }
    public HttpStatusCode StatusCode { get; }
    public Enum ErrorCode { get; }
    public string Scenario { get; set; }

    public UnsupportedMediaTypeResponse()
    {
      StatusCode = HttpStatusCode.UnsupportedMediaType;
      ErrorCode = JsonReader.DeserializationFailed;
      Scenario = "Media Type requested is currently not supported.";
    }
  }
}