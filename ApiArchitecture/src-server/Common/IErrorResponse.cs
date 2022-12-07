using System;
using System.Net;

namespace Jda.WfmEssApi.Common
{
  public interface IErrorResponse
  {
    Enum SourceErrorCode { get; }
    HttpStatusCode StatusCode { get; }
    Enum ErrorCode { get; }
    string Scenario { get; }
  }
}
