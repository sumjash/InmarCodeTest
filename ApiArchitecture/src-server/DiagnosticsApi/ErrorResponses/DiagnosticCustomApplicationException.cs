using System;
using System.Net;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.ErrorResponses
{
  public class DiagnosticCustomApplicationException : IErrorResponse
  {
    public HttpStatusCode StatusCode { get; }
    public Enum ErrorCode { get; }
    public Enum SourceErrorCode { get; }
    public string Scenario { get; }

    public DiagnosticCustomApplicationException()
    {
      StatusCode = HttpStatusCode.Conflict;
      ErrorCode = DiagnosticError.DiagnosticErrorThrownFromMethod;
      SourceErrorCode = DiagnosticError.DiagnosticErrorThrownFromMethod;
      Scenario = "Diagnostics CustomApplicationException.";
    }
  }
}