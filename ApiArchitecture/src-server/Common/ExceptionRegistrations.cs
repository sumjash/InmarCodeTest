using System;
using System.Net;
using RP.DomainModel.Scheduling;
using RP.DomainModel.Services.Diagnostics;

namespace Jda.WfmEssApi.Common
{
  public class ExceptionRegistrations
  {
    public static void RegisterExceptionCodeMapping(Enum domainVal, Enum apiVal,
      HttpStatusCode status = HttpStatusCode.Conflict)
    {
      ExceptionRegistry.RegisterExceptionCodeMapping(domainVal, apiVal, status);
    }

    public static void RegisterArgumentKeyMapping(Enum domainVal, Enum apiVal)
    {
      ExceptionRegistry.RegisterArgumentKeyMapping(domainVal, apiVal);
    }

    public void RegisterApiExceptions()
    {
      RegisterExceptionCodeMapping(DoeDiagnosticErrors.DoeFromClassicCreate,
        ApiExceptionCode.CaeDiagnosticErrors.CaeOperationExceptionFromClassicCreate);
      RegisterExceptionCodeMapping(DoeDiagnosticErrors.DoeFromClassicRead,
        ApiExceptionCode.CaeDiagnosticErrors.CaeOperationExceptionFromClassicRead);
      RegisterExceptionCodeMapping(DoeDiagnosticErrors.DoeFromClassicUpdate,
        ApiExceptionCode.CaeDiagnosticErrors.CaeOperationExceptionFromClassicUpdate);
      RegisterExceptionCodeMapping(DoeDiagnosticErrors.DoeFromClassicDelete,
        ApiExceptionCode.CaeDiagnosticErrors.CaeOperationExceptionFromClassicDelete);
    }
    public void RegisterApiExceptionArguments()
    {
      
    }
  }
}