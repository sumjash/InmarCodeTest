using System.Collections.Generic;
using Perigee;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi
{
  public class DiagnosticCollectionResource : IApiCollectionResource<DiagnosticResource>, IApiGettableResourceV1, IEntityResource
  {
    public bool HasRecords()
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<DiagnosticResource> Entities { get; set; }
    public string Self { get; }
    public string IdAsString { get; }
  }
}