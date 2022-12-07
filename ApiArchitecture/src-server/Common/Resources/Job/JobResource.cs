namespace Jda.WfmEssApi.Common.Resources.Job
{
  public class JobResource
  {

    /// <summary>
    /// The job's internal Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The name of the job
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// the color of the job
    /// </summary>
    public string JobColor { get; set; }
    /// <summary>
    /// the flag of primary status of the job
    /// </summary>
    public bool IsPrimary { get; set; }

    /// <summary>
    /// the flag of assignment of the job
    /// </summary>
    public bool IsAssigned { get; set; }
  }
}