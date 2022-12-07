using System.Collections.Generic;

namespace Jda.WfmEssApi.Common
{
  public interface IApiCollectionResource<T> : IApiCollectionResource
  {
    IEnumerable<T> Entities { get; set; }
  }

  public interface IApiCollectionResource
  {
    bool HasRecords();
  }
}