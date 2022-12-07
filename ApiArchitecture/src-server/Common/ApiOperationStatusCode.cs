namespace Jda.WfmEssApi.Common
{
  public enum ApiOperationStatusCode
  {
    Found,
    Created,
    Updated,
    Deleted,
    NotFound,
    Exception,
    EmptyCollection,
    UnmappedDomainError,
    Queued,
    BadRequest,
    InternalServerError 
  }
}