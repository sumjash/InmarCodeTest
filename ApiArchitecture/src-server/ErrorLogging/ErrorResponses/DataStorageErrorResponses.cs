using System;
using System.Net;
using Jda.WfmEssApi.Common;
using RP.DomainModel.Common.NHibernateAbstracts;

namespace Jda.WfmEssApi.ErrorLogging.ErrorResponses
{
  public enum ApiDataStorageErrorCodes
  {
    NotFound,
    BadRequest
  }

  public class DataStorageEntityNotFound : IErrorResponse
  {
    public Enum SourceErrorCode { get; }
    public HttpStatusCode StatusCode { get; }
    public Enum ErrorCode { get; }
    public string Scenario { get; }

    public DataStorageEntityNotFound()
    {
      SourceErrorCode = DataStorageErrorCodes.EntityNotFound;
      StatusCode = HttpStatusCode.NotFound;
      ErrorCode = ApiDataStorageErrorCodes.NotFound;
      Scenario = "Data storage reports the entity requested does not currently exist.";
    }
  }
}