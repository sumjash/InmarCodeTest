using System;
using System.Collections.Generic;

namespace Jda.WfmEssApi.Common
{
  public class TaskError
  {
    public string ErrorCode { get; set; }
    public string UserMessage { get; set; }
    public string For { get; set; }

    public TaskError()
    {

    }

    public TaskError(string errorCode, string userMessage, string forIndex)
    {
      ErrorCode = errorCode;
      UserMessage = userMessage;
      For = forIndex;
    }
  }
}