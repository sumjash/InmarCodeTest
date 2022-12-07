namespace Jda.WfmEssApi.Helpers
{
  public static class ErrorMessage
  {
    public const string NotFoundMessage = "The ~Resource~ for the {0} of {1} was not found.";
    public const string NotFoundMessageForMultiple = "The ~Resource~ for the {0} in '{1}' was not found.";

    public const string NotFoundMessageForTwoParameters =
      "The ~Resource~ for the {0} of {1} and {2} of {3} was not found.";

    public const string NotAssociatedMessage = "There were no ~Resource~s associated with the {0} of {1}.";

    public const string NoAssociationBetweenTwoParameters =
      "The ~Resource~ with a {0} of {1} is not associated with the {2} of {3}.";

    public const string NotSetupMessage = "No ~Resource~s were found.";

    public const string IDParameterDoesNotMatchResourceID =
      "The Parameter {0} ID of {1} does not match the ID of {2} on the ~Resource~.";

    public const string CustomFieldValueStringLengthError = "The Custom Field Value cannot exceed 255 characters.";
    public const string EmployeeAlreadyExistsMessage = "The employee with Employee ID of {0} already exists.";
    public const string StartCannotBeAfterEnd = "The start ({0}) must before end ({1}).";
    public const string ShouldHave = "The ~Resource~ should have {0}";
    public const string DuplicateDetails = "The ~Resource~ should not have details with same date";
    public const string DateIsOutOfRange = "The ~Resource~ date ({0}) should be in between {1} and {2}";
    public const string TotalShouldMatch = "The ~Resource~ {0} is not equal to sum of {1} in {2}";
    public const string EmployeeIDShouldNotBeModified = "Employee ID Should not be modified while Update.";
    public const string CannotUpdateToPending = "The ~Resource~ status cannot be changed from {0} to Pending";
    public const string ProvideRequiredIdsForBackPay = "Provide all required Ids for BackPayMapping";
    public const string UserIDInUriAndResourceNotMatching =
      "The Parameter {0} UserID of {1} does not match the UserID of {2} on the ~Resource~.";
  }
}