using RP.DomainModel.Common;

namespace Jda.WfmEssApi.Common
{
  /// <summary>
  /// Base Interface for any "Operation": Command (write) or Query (read). 
  /// Likely not used in code except to communicate intent and possibly for future meta-analysis
  /// </summary>
  public interface IApiOperation<out T> : IApiOperation
  {
    T Run(IUnitOfWork unitOfWork);
  }

  public interface IApiOperation
  {

  }
}

