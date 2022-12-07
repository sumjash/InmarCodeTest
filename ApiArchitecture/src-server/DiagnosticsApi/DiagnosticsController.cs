using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Perigee.GlobalErrorHandling;
using Jda.WfmEssApi.Common;
using RP.DomainModel.Services.Diagnostics;
using Jda.WfmEssApi.DiagnosticsApi.ErrorResponses;
using Jda.WfmEssApi.Helpers;
using Jda.WfmEssApi.Services;
using RPCore.Time;

namespace Jda.WfmEssApi.DiagnosticsApi
{
  public class DiagnosticsController : BaseEssApiController
  {
    // - Protocol Enforcer Execution
    protected readonly IApiProtocolEnforcerV1BetaX ProtocolEnforcer;

    protected readonly DiagnosticApiOperationCoordinatorFactory OperationCoordinatorFactory;

    public DiagnosticsController(ApiProtocolEnforcerV1BetaX protocolEnforcer,
      DiagnosticApiOperationCoordinatorFactory operationCoordinatorFactory)
    {
      ProtocolEnforcer = protocolEnforcer;
      OperationCoordinatorFactory = operationCoordinatorFactory;
    }

    [Route("~/api/v1-beta2/diagnostics")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [ResponseType(typeof(DiagnosticMethodCollectionResource))]
    public HttpResponseMessage GetAllAvailableDiagnostics()
    {
      return ReadEntity(() =>
        {
          var mapper = new DiagnosticsMapper(UnitOfWork);
          return mapper.GetAvailableDiagnostics();
        }
      );
    }

    //Scenario 1 - API with Exception from within Controller method
    [Route("~/api/v1-beta2/diagnostics/exceptionInController")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage ExceptionInController()
    {
      throw new Exception("Diagnostics - A generic exception was thrown within controller method.");
    }

    //Scenario 2 - Classic Execution Create
    [Route("~/api/v1-beta2/diagnostics/exceptionInClassicCreate")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage ExceptionInClassicCreate()
    {
      return CreateEntity(() =>
        {
          var mapper = new DiagnosticsMapper(UnitOfWork);
          return mapper.GetExceptionCreate();
        }
      );
    }


    //Scenario 3 - Classic Execution Read
    [Route("~/api/v1-beta2/diagnostics/exceptionInClassicRead")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage ExceptionInClassicRead()
    {
      return ReadEntity(() =>
        {
          var mapper = new DiagnosticsMapper(UnitOfWork);
          return mapper.GetExceptionRead();
        },
        "DiagnosticResourceNotFound", ErrorMessage.NotFoundMessage, "Diagnostic Resource", 0);
    }


    //Scenario 4 - Classic Execution Update
    [Route("~/api/v1-beta2/diagnostics/exceptionInClassicUpdate")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPut]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage ExceptionInClassicUpdate()
    {
      return UpdateEntity(() =>
        {
          var mapper = new DiagnosticsMapper(UnitOfWork);
          return mapper.GetExceptionUpdate();
        },
        "DiagnosticResourceNotFound", ErrorMessage.NotFoundMessage, "Diagnostic Resource", 0);
    }


    //Scenario 5 - Classic Execution Delete
    [Route("~/api/v1-beta2/diagnostics/exceptionInClassicDelete")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage ExceptionInClassicDelete()
    {
      return DeleteEntity(() =>
      {
        var mapper = new DiagnosticsMapper(UnitOfWork);
        return mapper.GetExceptionDelete();
      });
    }

    //Scenario 6 - (PRO. ENF.) API with Exception from within Single Create
    [Route("~/api/v1-beta2/diagnostics/exceptionOnSingleResourceCreate")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage ExceptionOnSingleResourceCreate()
    {
      InitializeRequestToControllerDependencies();
      var exceptionOnSingleResourceCreate =
        OperationCoordinatorFactory.CreateGetDiagnosticExceptionOnSingleResourceCreateOperation();
      return ProtocolEnforcer.Create(exceptionOnSingleResourceCreate);
    }

    //Scenario 7 - (PRO. ENF.) API with Exception from within Single Read
    [Route("~/api/v1-beta2/diagnostics/exceptionOnSingleResourceRead")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage ExceptionOnSingleResourceRead()
    {
      InitializeRequestToControllerDependencies();
      var exceptionOnSingleResourceRead =
        OperationCoordinatorFactory.CreateGetDiagnosticExceptionOnSingleResourceReadOperation();
      return ProtocolEnforcer.Read(exceptionOnSingleResourceRead);
    }

    //Scenario 8 - (PRO. ENF.) API with Exception from within Single Update
    [Route("~/api/v1-beta2/diagnostics/exceptionOnSingleResourceUpdate")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPut]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage ExceptionOnSingleResourceUpdate()
    {
      InitializeRequestToControllerDependencies();
      var exceptionOnSingleResourceUpdate =
        OperationCoordinatorFactory.CreateGetDiagnosticExceptionOnSingleResourceUpdateOperation();
      return ProtocolEnforcer.Update(exceptionOnSingleResourceUpdate);
    }

    //Scenario 9 - (PRO. ENF.) API with Exception from within Single Delete
    [Route("~/api/v1-beta2/diagnostics/exceptionOnSingleResourceDelete")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage ExceptionOnSingleResourceDelete()
    {
      InitializeRequestToControllerDependencies();
      var exceptionOnSingleResourceDelete =
        OperationCoordinatorFactory.CreateGetDiagnosticExceptionOnSingleResourceDeleteOperation();
      return ProtocolEnforcer.Delete(exceptionOnSingleResourceDelete);
    }

    //Scenario 10 - (PRO. ENF.) API with Exception from within Collection Read
    [Route("~/api/v1-beta2/diagnostics/exceptionOnCollectionResourceRead")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage ExceptionOnCollectionResourceRead()
    {
      InitializeRequestToControllerDependencies();
      var exceptionOnCollectionResourceRead =
        OperationCoordinatorFactory.CreateGetDiagnosticExceptionOnCollectionResourceReadOperation();
      return ProtocolEnforcer.Read(exceptionOnCollectionResourceRead);
    }

    protected void InitializeRequestToControllerDependencies()
    {
      ProtocolEnforcer.SetRequest(Request);
      ProtocolEnforcer.SetUrlHelper(Url);
    }

    /** CustomApplicationException Scenarios */
    // Scenario 13
    [Route("~/api/v1-beta2/diagnostics/cae/exceptionWithinMethod")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [ResponseType(typeof(EssApiErrorContainer))]
    [ErrorResponses(typeof(DiagnosticCustomApplicationException))]
    public HttpResponseMessage CustomApplicationExceptionWithinMethod()
    {
      throw new CustomApplicationException<DiagnosticError>(DiagnosticError.DiagnosticErrorThrownFromMethod);
    }

    //Scenario 14 - Classic Execution Create CAE
    [Route("~/api/v1-beta2/diagnostics/cae/exceptionInClassicCreate")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage CustomApplicationExceptionInClassicCreate()
    {
      return CreateEntity(() =>
      {
        var mapper = new DiagnosticsMapper(UnitOfWork);
        return mapper.CustomAppCreateException();
      });
    }


    //Scenario 15 - Classic Execution Read CAE
    [Route("~/api/v1-beta2/diagnostics/cae/exceptionInClassicRead")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage CustomApplicationExceptionInClassicRead()
    {
      return ReadEntity(() =>
        {
          var mapper = new DiagnosticsMapper(UnitOfWork);
          return mapper.CustomAppReadException();
        },
        "DiagnosticResourceNotFound", ErrorMessage.NotFoundMessage, "Diagnostic Resource", 0);
    }


    //Scenario 16 - Classic Execution Update CAE
    [Route("~/api/v1-beta2/diagnostics/cae/exceptionInClassicUpdate")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPut]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage CustomApplicationExceptionInClassicUpdate()
    {
      return UpdateEntity(() =>
        {
          var mapper = new DiagnosticsMapper(UnitOfWork);
          return mapper.CustomAppUpdateException();
        },
        "DiagnosticResourceNotFound", ErrorMessage.NotFoundMessage, "Diagnostic Resource", 0);
    }


    //Scenario 17 - Classic Execution Delete CAE
    [Route("~/api/v1-beta2/diagnostics/cae/exceptionInClassicDelete")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage CustomApplicationExceptionInClassicDelete()
    {
      return DeleteEntity(() =>
      {
        var mapper = new DiagnosticsMapper(UnitOfWork);
        return mapper.CustomAppDeleteException();
      });
    }


    //Scenario 18 - (PRO. ENF.) API with CAE from within Single Create
    [Route("~/api/v1-beta2/diagnostics/cae/exceptionOnSingleResourceCreate")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage CustomApplicationExceptionOnSingleResourceCreate()
    {
      InitializeRequestToControllerDependencies();
      var exceptionOnSingleResourceCreate =
        OperationCoordinatorFactory.CreateGetCustomApplicationExceptionOnSingleResourceCreateOperation();
      return ProtocolEnforcer.Create(exceptionOnSingleResourceCreate);
    }

    //Scenario 19 - (PRO. ENF.) API with CAE from within Single Read
    [Route("~/api/v1-beta2/diagnostics/cae/exceptionOnSingleResourceRead")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage CustomApplicationExceptionOnSingleResourceRead()
    {
      InitializeRequestToControllerDependencies();
      var exceptionOnSingleResource =
        OperationCoordinatorFactory.CreateGetCustomApplicationExceptionOnSingleResourceReadOperation();
      return ProtocolEnforcer.Read(exceptionOnSingleResource);
    }

    //Scenario 20 - (PRO. ENF.) API with CAE from within Single Update
    [Route("~/api/v1-beta2/diagnostics/cae/exceptionOnSingleResourceUpdate")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPut]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage CustomApplicationExceptionOnSingleResourceUpdate()
    {
      InitializeRequestToControllerDependencies();
      var customApplicationExceptionOnSingleResource =
        OperationCoordinatorFactory.CreateGetCustomApplicationExceptionOnSingleResourceUpdateOperation();
      return ProtocolEnforcer.Update(customApplicationExceptionOnSingleResource);
    }

    //Scenario 21 - (PRO. ENF.) API with CAE from within Single Delete
    [Route("~/api/v1-beta2/diagnostics/cae/exceptionOnSingleResourceDelete")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage CustomApplicationExceptionOnSingleResourceDelete()
    {
      InitializeRequestToControllerDependencies();
      var customApplicationExceptionOnSingle =
        OperationCoordinatorFactory.CreateGetCustomApplicationExceptionOnSingleResourceDeleteOperation();
      return ProtocolEnforcer.Delete(customApplicationExceptionOnSingle);
    }

    //Scenario 22 - (PRO. ENF.) API with CAE from within Collection Read
    [Route("~/api/v1-beta2/diagnostics/cae/exceptionOnCollectionResourceRead")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage CustomApplicationExceptionOnCollectionResourceRead()
    {
      InitializeRequestToControllerDependencies();
      var customApplicationExceptionOnCollectionResource =
        OperationCoordinatorFactory.CreateGetCustomApplicationExceptionOnCollectionResourceReadOperation();
      return ProtocolEnforcer.Read(customApplicationExceptionOnCollectionResource);
    }

    /**Domain Operation Exception Scenarios*/
    //Scenario 24 - API with DomainOperationException from within Controller method
    [Route("~/api/v1-beta2/diagnostics/doe/domainOperationExceptionInControllerMethod")]
    [ResponseType(typeof(EssApiErrorContainer))]
    [ApiExplorerSettings(IgnoreApi = true)]
    public void DomainOperationExceptionInControllerMethod()
    {
      DiagnosticService.ThrowDomainOperationException(DoeDiagnosticErrors.DoeThrownFromController);
    }

    //Scenario 25 - (CLASSIC) API with DomainOperationException from within Create
    [Route("~/api/v1-beta2/diagnostics/doe/domainOperationExceptionFromCreate")]
    [ResponseType(typeof(EssApiErrorContainer))]
    [ApiExplorerSettings(IgnoreApi = true)]
    public HttpResponseMessage DomainOperationExceptionFromCreate()
    {
      return CreateEntity(() =>
      {
        var mapper = new DiagnosticsMapper(UnitOfWork);
        return mapper.DomainOperationCreateException();
      });
    }

    //Scenario 26 - (CLASSIC) API with DomainOperationException from within Read
    [Route("~/api/v1-beta2/diagnostics/doe/domainOperationExceptionFromRead")]
    [ResponseType(typeof(EssApiErrorContainer))]
    [ApiExplorerSettings(IgnoreApi = true)]
    public HttpResponseMessage DomainOperationExceptionFromRead()
    {
      return ReadEntity(() =>
        {
          var mapper = new DiagnosticsMapper(UnitOfWork);
          return mapper.DomainOperationReadException();
        },
        "DiagnosticResourceNotFound", ErrorMessage.NotFoundMessage, "Diagnostic Resource", 0);
    }

    //Scenario 27 - (CLASSIC) API with DomainOperationException from within Update
    [Route("~/api/v1-beta2/diagnostics/doe/domainOperationExceptionFromUpdate")]
    [ResponseType(typeof(EssApiErrorContainer))]
    [ApiExplorerSettings(IgnoreApi = true)]
    public HttpResponseMessage DomainOperationExceptionFromUpdate()
    {
      return UpdateEntity(() =>
        {
          var mapper = new DiagnosticsMapper(UnitOfWork);
          return mapper.DomainOperationUpdateException();
        },
        "DiagnosticResourceNotFound", ErrorMessage.NotFoundMessage, "Diagnostic Resource", 0);
    }

    //Scenario 28 - (CLASSIC) API with DomainOperationException from within Delete
    [Route("~/api/v1-beta2/diagnostics/doe/domainOperationExceptionFromDelete")]
    [ResponseType(typeof(EssApiErrorContainer))]
    [ApiExplorerSettings(IgnoreApi = true)]
    public HttpResponseMessage DomainOperationExceptionFromDelete()
    {
      return DeleteEntity(() =>
      {
        var mapper = new DiagnosticsMapper(UnitOfWork);
        return mapper.DomainOperationDeleteException();
      });
    }

    //Scenario 29 - (PRO. ENF.) API with DOE from within Single Create
    [Route("~/api/v1-beta2/diagnostics/doe/domainOperationExceptionOnSingleResourceCreate")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage DomainOperationExceptionOnSingleResourceCreate()
    {
      InitializeRequestToControllerDependencies();
      var singleCreate = OperationCoordinatorFactory.CreateGetDomainOperationExceptionOnSingleResourceCreateOperation();
      return ProtocolEnforcer.Create(singleCreate);
    }

    //Scenario 30 - (PRO. ENF.) API with DOE from within Single Read
    [Route("~/api/v1-beta2/diagnostics/doe/domainOperationExceptionOnSingleResourceRead")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage DomainOperationExceptionOnSingleResourceRead()
    {
      InitializeRequestToControllerDependencies();
      var singleRead = OperationCoordinatorFactory.CreateGetDomainOperationExceptionOnSingleResourceReadOperation();
      return ProtocolEnforcer.Read(singleRead);
    }

    //Scenario 31 - (PRO. ENF.) API with DOE from within Single Update
    [Route("~/api/v1-beta2/diagnostics/doe/domainOperationExceptionOnSingleResourceUpdate")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPut]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage DomainOperationExceptionOnSingleResourceUpdate()
    {
      InitializeRequestToControllerDependencies();
      var domainOperationExceptionOnSingleResource =
        OperationCoordinatorFactory.CreateGetDomainExceptionOnSingleResourceUpdateOperation();
      return ProtocolEnforcer.Update(domainOperationExceptionOnSingleResource);
    }

    //Scenario 32 - (PRO. ENF.) API with DOE from within Single Delete
    [Route("~/api/v1-beta2/diagnostics/doe/domainOperationExceptionOnSingleResourceDelete")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage DomainOperationExceptionOnSingleResourceDelete()
    {
      InitializeRequestToControllerDependencies();
      var domainOperationExceptionOnSingleResource =
        OperationCoordinatorFactory.CreateGetDomainOperationExceptionOnSingleResourceDelete();
      return ProtocolEnforcer.Delete(domainOperationExceptionOnSingleResource);
    }

    //Scenario 33 - (PRO. ENF.) API with DOE from within Collection Read
    [Route("~/api/v1-beta2/diagnostics/doe/domainOperationExceptionOnCollectionResourceRead")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage DomainOperationExceptionOnCollectionResourceRead()
    {
      InitializeRequestToControllerDependencies();
      var domainOperationExceptionOnCollectionResource =
        OperationCoordinatorFactory.CreateGetDomainOperationExceptionOnCollectionResourceReadOperation();
      return ProtocolEnforcer.Read(domainOperationExceptionOnCollectionResource);
    }

    [Route("~/api/v1-beta2/diagnostics/throwUnitOfWorkError")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [ResponseType(typeof(EssApiErrorContainer))]
    public HttpResponseMessage ThrowUnitOfWorkError()
    {
      InitializeRequestToControllerDependencies();
      // CreateGetDomainOperationExceptionOnSingleResourceCreateOperation();
      var singleCreate = OperationCoordinatorFactory.CreateApiThrowUnitOfWorkErrorByParameter();
      return ProtocolEnforcer.Create(singleCreate);

      /*
        Use this method to throw an exception from the Unit of Work area with the format:
        ~/api/diagnostics/throwUnitOfWorkError?runDiagnostic={scenario}
        Where {scenario} is one of the following:
          NoException,
          ExceptionFromUnitOfWorkStart,
          ExceptionAfterUnitOfWorkStart,
          UnauthorizedAccessExceptionFromUnitOfWorkStart,
          UnauthorizedAccessExceptionAfterUnitOfWorkStart,
          DomainOperationExceptionDuringUnitOfWorkCommit,
          ExceptionBeforeUnitOfWorkCommit,
          ExceptionBeforeUnitOfWorkStart,
          CustomApplicationExceptionDuringUnitOfWorkCommit
          ExceptionInFilter
       */
    }

    //Scenario 75 - API with DomainOperationException during UnitOfWork commit
    [Route("~/api/v1-beta2/diagnostics/doe/domainOperationExceptionDuringCommit")]
    [ResponseType(typeof(EssApiErrorContainer))]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    public HttpResponseMessage DomainOperationExceptionDuringCommit()
    {
      return CreateEntity(() =>
      {
        DiagnosticService.ThrowDomainOperationExceptionOnCommit();
        var mapper = new DiagnosticsMapper(UnitOfWork);
        return mapper.StartHandleCommit();
      });
    }

    //Scenario 77 API with Exception during UnitOfWork commit
    [Route("~/api/v1-beta2/diagnostics/exceptionDuringCommit")]
    [ResponseType(typeof(EssApiErrorContainer))]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    public HttpResponseMessage ExceptionDuringCommit()
    {
      return CreateEntity(() =>
      {
        DiagnosticService.ThrowExceptionOnCommit();
        var mapper = new DiagnosticsMapper(UnitOfWork);
        return mapper.StartHandleCommit();
      });
    }

    //Scenario 53 API with Exception during Serializaiton
    [Route("~/api/v1-beta2/diagnostics/formatExceptionDuringSerialization")]
    [ResponseType(typeof(EssApiErrorContainer))]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public HttpResponseMessage FormatExceptionDuringSerialization()
    {
      return Request.CreateResponse(HttpStatusCode.OK, new Diagnostics.FailingStructResource());
    }

    //Scenario 41 API with SQL exception from NHibernate code
    [Route("~/api/v1-beta2/diagnostics/nHiberbernateSqlException")]
    [ResponseType(typeof(EssApiErrorContainer))]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public void NHiberbernateSqlException()
    {
      UnitOfWork.AddQueryForTransaction(UnitOfWork.GetSession().CreateQuery("from ClassThatDoesNotExist"));
    }

    //Scenario 45 API with a date path parameter that doesn't match the pattern
    [Route("~/api/v1-beta2/diagnostics/dateParameterPatternMismatch/{date}")]
    [ResponseType(typeof(EssApiErrorContainer))]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public HttpResponseMessage DateParameterPatternMismatch(BusinessDate date)
    {
      return Request.CreateResponse(HttpStatusCode.OK);
    }
  }
}