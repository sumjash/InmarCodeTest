using System.Collections.Generic;
using System.Linq;
using RP.DomainModel.Common;

namespace Jda.WfmEssApi.Common
{
  public class ExceptionArgumentTranslator
  {
    public static IEnumerable<IApiExceptionArgument> GetApiExceptionArguments(DomainOperationException domainException)
    {
      var arguments = domainException.OperationEnumArguments;
      if (!arguments.Any())
      {
        return null;
      }

      var apiExceptionArguments = new List<IApiExceptionArgument>();
      foreach (var argument in arguments)
      {
        var apiExceptionArgument = ExceptionRegistry.GetApiExceptionArgument(argument.Key);
        apiExceptionArgument.Value = argument.Value;
        apiExceptionArguments.Add(apiExceptionArgument);
      }
      return apiExceptionArguments;
    }
  }
}
