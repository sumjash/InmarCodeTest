using System;
using System.Collections.Generic;

namespace Jda.WfmEssApi.Common
{

  public enum OverallBatchStatus
  {
    Accepted,
    Rejected,
    Mixed
  }

  public class BatchEndpointResponse
  {
    public Guid ResponseId{ get; set; }
    public string ErrorTimestamp { get; set; }
    public string OverallStatus{ get; set; }
    public long TasksReceived{ get; set; }
    public long TasksAccepted{ get; set; }
    public long TasksRejected{ get; set; }
    public List<TaskError> Errors { get; set; }

    public BatchEndpointResponse()
    {
      Errors = new List<TaskError>();
      ResponseId = Guid.NewGuid();
      ErrorTimestamp =  DateTime.UtcNow.ToString("o");
      OverallStatus = OverallBatchStatus.Accepted.ToString();
    }

    public bool HasRejectedTask()
    {
      return !OverallStatus.Equals(OverallBatchStatus.Accepted.ToString());
    }
  }

}