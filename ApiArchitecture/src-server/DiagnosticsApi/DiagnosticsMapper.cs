using System;
using System.Collections.Generic;
using Perigee;
using Perigee.GlobalErrorHandling;
using RP.DomainModel.Common;
using RP.DomainModel.Diagnostic;
using RP.DomainModel.Services.Diagnostics;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi
{
  public class DiagnosticsMapper : IMapper<DiagnosticEntity, DiagnosticResource>,
    IMapper<IEnumerable<DiagnosticEntity>, DiagnosticCollectionResource>
  {
    private readonly IUnitOfWork _Uow;

    public DiagnosticsMapper(IUnitOfWork uow)
    {
      _Uow = uow;
    }

    public DiagnosticsMapper() { }

    internal DiagnosticResource GetExceptionCreate()
    {
      throw new Exception("Diagnostics - A generic exception was thrown from a classic form of a Create Command.");
    }

    internal DiagnosticResource GetExceptionRead()
    {
      throw new Exception("Diagnostics - A generic exception was thrown from a classic form of a Read Query.");
    }

    internal DiagnosticResource GetExceptionUpdate()
    {
      throw new Exception("Diagnostics - A generic exception was thrown from a classic form of an Update Command.");
    }
    internal DiagnosticResource GetExceptionDelete()
    {
      throw new Exception("Diagnostics - A generic exception was thrown from a classic form of a Delete Command.");
    }

    internal DiagnosticResource CustomAppCreateException()
    {
      throw new CustomApplicationException<DiagnosticError>(DiagnosticError.DiagnosticErrorFromClassicCreate);
    }

    internal DiagnosticResource CustomAppReadException()
    {
      throw new CustomApplicationException<DiagnosticError>(DiagnosticError.DiagnosticErrorFromClassicRead);
    }

    internal DiagnosticResource CustomAppUpdateException()
    {
      throw new CustomApplicationException<DiagnosticError>(DiagnosticError.DiagnosticErrorFromClassicUpdate);
    }

    internal DiagnosticResource CustomAppDeleteException()
    {
      throw new CustomApplicationException<DiagnosticError>(DiagnosticError.DiagnosticErrorFromClassicDelete);
    }

    internal DiagnosticResource DomainOperationCreateException()
    {
      return (DiagnosticResource) DiagnosticService.ThrowDomainOperationException(
        DoeDiagnosticErrors.DoeFromClassicCreate);
    }

    internal DiagnosticResource DomainOperationReadException()
    {
      return (DiagnosticResource)DiagnosticService.ThrowDomainOperationException(
        DoeDiagnosticErrors.DoeFromClassicRead);
    }

    public DiagnosticResource DomainOperationUpdateException()
    {
      return (DiagnosticResource)DiagnosticService.ThrowDomainOperationException(
        DoeDiagnosticErrors.DoeFromClassicUpdate);
    }

    public IEntityResource DomainOperationDeleteException()
    {
      return (DiagnosticResource)DiagnosticService.ThrowDomainOperationException(
        DoeDiagnosticErrors.DoeFromClassicDelete);
    }

    internal DiagnosticResource StartHandleCommit()
    {
      _Uow.Commit();
      return new DiagnosticResource();
    }

    public DiagnosticMethodCollectionResource GetAvailableDiagnostics()
    {
      return new DiagnosticMethodCollectionResource();
    }

    public DiagnosticResource Map(DiagnosticEntity entity)
    {
      throw new NotImplementedException();
    }

    public DiagnosticCollectionResource Map(IEnumerable<DiagnosticEntity> entity)
    {
      throw new NotImplementedException();
    }
  }
}