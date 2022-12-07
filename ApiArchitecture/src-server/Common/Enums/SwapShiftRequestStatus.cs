namespace Jda.WfmEssApi.Common.Enums
{
  public enum SwapShiftRequestStatus
  {
    AwaitingManager = 'y',
    RecipientDenied = 'n',
    SwapPending = 'r',
    ManagerApproved = 'a',
    ManagerDenied = 'x',
    SenderCanceled = 'c',
    Canceled = 'i'
  }
}