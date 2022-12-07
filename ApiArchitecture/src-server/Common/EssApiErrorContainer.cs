using Newtonsoft.Json;
using Perigee;
using RP.DomainModel.Common;

namespace Jda.WfmEssApi.Common
{
  public class EssApiErrorContainer : ApiErrorContainerV2
  {
    [JsonIgnore]
    public DomainOperationException Exception { get; set; } 

    public EssApiErrorContainer(DomainOperationException ex, string userMessage, string errorCode, 
      object devMessage = null, object moreInfo = null) 
      : base(userMessage, errorCode, devMessage, moreInfo)
    {
      Exception = ex;
    }

    public EssApiErrorContainer(string userMessage, string errorCode,
    object devMessage = null, object moreInfo = null)
      : base(userMessage, errorCode, devMessage, moreInfo)
    {
     
    }
  }
}