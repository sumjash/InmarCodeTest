using Jda.WfmEssApi.ApiExceptionCode;
using Jda.WfmEssApi.Common;
using RP.DomainModel.Common.Types;
using System;
using System.Net;

namespace Jda.WfmEssApi.Common
{
  public class StringCouldNotBeConvertedToInt : IErrorResponse
  {
    public Enum SourceErrorCode { get; }

    public HttpStatusCode StatusCode { get; }

    public Enum ErrorCode { get; }

    public string Scenario { get; }

    public StringCouldNotBeConvertedToInt()
    {
      StatusCode = HttpStatusCode.Conflict;
      SourceErrorCode = InputErrorCodes.StringCouldNotBeConvertedToInt;
      ErrorCode = CommonErrorCodes.StringCouldNotBeConvertedToInt;
      Scenario = "String Could Not Be Converted To Int";
    }
  }
}