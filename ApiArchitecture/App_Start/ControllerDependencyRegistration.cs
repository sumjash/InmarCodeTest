using Jda.WfmEssApi.Common;
using Jda.WfmEssApi.DiagnosticsApi;
using System.Web.Http.Dependencies;

namespace Jda.WfmEssApi
{
  public static class ControllerDependencyRegistration
  {
    public static OverriddenWebApiDependencyResolver CreateDependencyResolver(IDependencyResolver currentResolver)
    {
        return
            new OverriddenWebApiDependencyResolver(currentResolver)

                .Add(typeof(DiagnosticsController),
                    () => new DiagnosticsController(new ApiProtocolEnforcerV1BetaX(),
                        new DiagnosticApiOperationCoordinatorFactory()))

    }
  }
}