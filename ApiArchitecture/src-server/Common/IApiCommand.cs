using RP.DomainModel.Common;

namespace Jda.WfmEssApi.Common
{
  public interface IApiCommand : IApiOperation
  {
    ApiOperationResult Execute(IUnitOfWork unitOfWork);
  }
}