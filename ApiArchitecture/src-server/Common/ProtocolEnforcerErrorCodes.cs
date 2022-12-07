namespace Jda.WfmEssApi.Common
{
  public enum ProtocolEnforcerErrorCodes
  {
    InvalidCreateDuringReadOperation,
    InvalidUpdateDuringReadOperation,
    InvalidDeleteDuringReadOperation,
    InvalidNotFoundStatusForCollectionResponse,
    InvalidQueuedDuringReadOperation,
    InvalidEmptyCollectionStatusFromSingleReadOperation,
    InvalidNullResponseForCollectionResponse,
    InvalidUpdatedOrDeletedDuringCreateOperation,
    InvalidCreatedOrUpdateDuringDeleteOperation,
    InvalidCreatedOrDeletedDuringUpdateOperation,
    InvalidNullReturnedDuringOperation,
    ReturnedResponseDoesNotImplementIApiGettableResource,
    ReturnedResponseDoesNotImplementIApiGettableResourceV1,
    UrlHelperNotSet,
    ReturnedResponseDoesNotContainSelfLink,
    ReturnedResponseShouldContainLinkCollection
  }
}