using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;
using Jda.WfmEssApi.DiagnosticsApi.Commands;
using Jda.WfmEssApi.DiagnosticsApi.Queries;

namespace Jda.WfmEssApi.DiagnosticsApi
{
  public class DiagnosticApiOperationFactory : ApiOperationsFactory<DiagnosticApiOperationFactory>
  {
    /* Single-Read */
    public static GetDiagnosticExceptionOnSingleResourceRead CreateDiagnosticExceptionOnSingleResourceRead()
    {
      return new GetDiagnosticExceptionOnSingleResourceRead();
    }
    public static GetCustomApplicationExceptionOnSingleResourceRead CreateCustomApplicationExceptionOnSingleResourceRead()
    {
      return new GetCustomApplicationExceptionOnSingleResourceRead();
    }
    public static GetDomainOperationExceptionOnSingleResourceRead CreateDomainOperationExceptionOnSingleResourceRead()
    {
      return new GetDomainOperationExceptionOnSingleResourceRead();
    }

    /* Collection-Read */
    public static GetDiagnosticExceptionOnCollectionResourceRead CreateDiagnosticExceptionOnCollectionResourceRead()
    {
      return new GetDiagnosticExceptionOnCollectionResourceRead();
    }
    public static GetCustomApplicationExceptionOnCollectionResourceRead CreateCustomApplicationExceptionOnCollectionResourceRead()
    {
      return new GetCustomApplicationExceptionOnCollectionResourceRead();
    }
    public static GetDomainOperationExceptionOnCollectionResourceRead CreateDomainOperationExceptionOnCollectionResourceRead()
    {
      return new GetDomainOperationExceptionOnCollectionResourceRead();
    }

    /* Create */
    public static GetDiagnosticExceptionOnSingleResourceCreate CreateDiagnosticExceptionOnSingleResourceCreate()
    {
      return new GetDiagnosticExceptionOnSingleResourceCreate();
    }
    public static GetCustomApplicationExceptionOnSingleResourceCreate CreateCustomApplicationExceptionOnSingleResourceCreate()
    {
      return new GetCustomApplicationExceptionOnSingleResourceCreate();
    }
    public static GetDomainOperationExceptionOnSingleResourceCreate CreateDomainOperationExceptionOnSingleResourceCreate()
    {
      return new GetDomainOperationExceptionOnSingleResourceCreate();
    }

    /* Update */
    public static GetDiagnosticExceptionOnSingleResourceUpdate CreateDiagnosticExceptionOnSingleResourceUpdate()
    {
      return new GetDiagnosticExceptionOnSingleResourceUpdate();
    }
    public static GetDomainExceptionOnSingleResourceUpdate CreateDomainExceptionOnSingleResourceUpdate()
    {
      return new GetDomainExceptionOnSingleResourceUpdate();
    }
    public static GetCustomApplicationExceptionOnSingleResourceUpdate CreateCustomApplicationExceptionOnSingleResourceUpdate()
    {
      return new GetCustomApplicationExceptionOnSingleResourceUpdate();
    }

    /* Delete */
    public static GetDiagnosticExceptionOnSingleResourceDelete CreateDiagnosticExceptionOnSingleResourceDelete()
    {
      return new GetDiagnosticExceptionOnSingleResourceDelete();
    }
    public static GetDomainOperationExceptionOnSingleResourceDelete CreateDomainOperationExceptionOnSingleResourceDelete()
    {
      return new GetDomainOperationExceptionOnSingleResourceDelete();
    }

    public static GetCustomApplicationExceptionOnSingleResourceDelete CreateCustomApplicationExceptionOnSingleResourceDelete()
    {
      return new GetCustomApplicationExceptionOnSingleResourceDelete();
    }

    public static ApiThrowUnitOfWorkErrorByParameter CreateApiThrowUnitOfWorkErrorByParameter()
    {
      return new ApiThrowUnitOfWorkErrorByParameter();
    }
  }
}