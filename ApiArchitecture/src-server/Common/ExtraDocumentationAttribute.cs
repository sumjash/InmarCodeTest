using System;

namespace Jda.WfmEssApi.Common
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class ExtraDocumentationAttribute : Attribute
  {
    public string ViewName { get; protected set; }

    public ExtraDocumentationAttribute(string viewName)
    {
      ViewName = viewName;
    }
  }
}
