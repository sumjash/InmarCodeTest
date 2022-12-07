using System.Globalization;

namespace Jda.WfmEssApi.Common.Resources.Job
{
  public class JobMapper : IMapper<RP.DomainModel.HumanResources.Job, JobResource>
  {
    public JobResource Map(RP.DomainModel.HumanResources.Job job)
    {
      var resource = new JobResource
      {
        Id = job.ID.ToString(CultureInfo.InvariantCulture),
        Name = job.Name,
        JobColor = job.HexColor
      };
      return resource;
    }
  }
}