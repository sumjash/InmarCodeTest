using RP.DomainModel.Common;

namespace Jda.WfmEssApi.Common
{
  public interface IApiQuery : IApiOperation
  {
    ApiOperationResult Fetch(IUnitOfWork unitOfWork);
  }
}