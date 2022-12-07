using System;
using System.ComponentModel.DataAnnotations;

namespace Jda.WfmEssApi.Common
{
  public class RequiredEnumAttribute : RequiredAttribute
  {
    public override bool IsValid(object value)
    {
      if (value == null)
      {
        return false;
      }
      var type = value.GetType();
      return type.IsEnum && Enum.IsDefined(type, value);
    }
  }
}