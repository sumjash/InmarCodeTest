﻿using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.OperationCoordinators
{
  public class GetDomainExceptionOnSingleResourceUpdateOperationCoordinator :
    OperationsCoordinatorUpdateSingle<DiagnosticEntity, DiagnosticResource, DiagnosticsMapper>
  {
    public override IApiOperation<DiagnosticEntity> GenerateOperation()
    {
      var operation = DiagnosticApiOperationFactory.CreateDomainExceptionOnSingleResourceUpdate();
      return operation;
    }
  }
}