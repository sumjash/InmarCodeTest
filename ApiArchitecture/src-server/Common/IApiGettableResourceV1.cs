using System;

namespace Jda.WfmEssApi.Common
{
  // IApiGettableResource uses 'meta' instead of 'self'.
  // So until all Api's are converted, will have to use this 
  // 'v1' version of the interface
  [Obsolete("IApiGettableResourceV1 is deprecated and should NO LONGER be used.")]
  public interface IApiGettableResourceV1
  {
    string Self { get; }
  }
}