using System;
using System.Net;
using Jda.WfmEssApi.Common;
using RP.DomainModel.Common.NHibernateAbstracts;

namespace Jda.WfmEssApi.ErrorLogging.ErrorResponses
{
  public class DataStorageAllOthers : IErrorResponse
  {
    public Enum SourceErrorCode { get; }
    public HttpStatusCode StatusCode { get; }
    public Enum ErrorCode { get; }
    public string Scenario { get; }

    public DataStorageAllOthers()
    {
      SourceErrorCode = DataStorageErrorCodes.StaleObjectStateException;
      StatusCode = HttpStatusCode.BadRequest;
      ErrorCode = ApiDataStorageErrorCodes.BadRequest;
      Scenario = "Data Storage reports bad request.";
    }
  }
}
