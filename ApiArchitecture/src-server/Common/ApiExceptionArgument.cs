using System;

namespace Jda.WfmEssApi.Common
{
  public class ApiExceptionArgument : IApiExceptionArgument
  {
    public Enum ApiExceptionArugmentCode { get; set; }

    public object Value { get; set; }

    protected string CollectionType => ApiExceptionArugmentCode.GetType().Name;

    protected string ExceptionType => ApiExceptionArugmentCode.ToString();
    
    public string GetArgumentCode()
    {
      return $"{CollectionType}.{ExceptionType}";
    }
  }
}
