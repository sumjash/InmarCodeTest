using Perigee;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi
{
  public class DiagnosticResource : IEntityResource, IApiGettableResourceV1
  {
    public string IdAsString { get; }
    public string Self { get; }
  }
}