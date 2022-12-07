namespace Jda.WfmEssApi.ApiExceptionCode
{
  public enum Default
  {
    Unsupported
  };

  public enum BorrowedSiteListError
  {
    BorrowedSiteListNameRequired,
    BorrowedSiteListNameShouldBeUnique,
    BorrowedSiteListShouldNotContainSiteLevel,
    BorrowedSiteListNameCannotBeModified,
    InvalidSiteGroups,
    InvalidOrganizationalUnits,
    InvalidBorrowedSiteListIds
  }

  public enum CaeDiagnosticErrors
  {
    CaeOperationExceptionFromClassicCreate,
    CaeOperationExceptionFromClassicRead,
    CaeOperationExceptionFromClassicUpdate,
    CaeOperationExceptionFromClassicDelete
  }

  public enum AdjustmentCategoryError
  {
    AdjustmentCategoryMustHaveName,
    AdjustmentCategoryMustHaveUniqueName,
    CannotSetItselfAsAlternativeAdjustmentCategory,
    AdjustmentCategoryNameTooLong,
    AdjustmentCategorySummaryCodeOneTooLong,
    AdjustmentCategorySummaryCodeTwoTooLong,
    AdjustmentCategoryWithNameAlreadyExists,
    AdjustmentCategoryIsUsed,
    DefaultVAlueShouldBeBetween0And24,
    WrongFieldCombination,
    ThresholdValueShouldBeBetween0And500
  }

  public enum PayCategoryError
  {
    PayCategoryMustHaveName,
    PayCategoryMustHaveUniqueName,
    CannotChangeRegisteredPayCategory,
    PayCategoryUsedInCategoryDefintion,
    PayCategoryNameTooLong,
    PayCategorySummaryCodeOneTooLong,
    PayCategorySummaryCodeTwoTooLong
  }

  public enum PayAttributeError
  {
    PayAttributeMustHaveName,
    PayAttributeWithNameAlreadyExists,
    PayAttributeWithIndicatorAlreadyExists,
    PayAttributeUsedInPayRule,
    PayAttributeMustHaveAttributeCode,
    PayAttributeNameTooLong
  }

  public enum PayRuleConfigurationError
  {
    PayRuleConfigurationMustHaveName,
    PayRuleConfigurationMustHaveUniqueName,
    PayRuleConfigurationNameTooLong
  }

  public enum EmployeeAccrualsError
  {
    AdjustmentCategoryIsNotAssignedToEmployee,
    EmployeeIdNotFound,
    TimeOffTypeIdNotFound
  }

  public enum EmployeeAttributeErrorCodes
  {
    EmployeeAttributeMustHaveUniqueName,
    EmployeeAttributeMustHaveUniqueCode,
    EmployeeAtrributeNameTooLong,
    EmployeeAttributeCodeTooLong,
    AssignmentsOfTheSameTypeCanNotOverlap,
    EmployeeAttributeIdNotFound,
    EmployeeAttributeMustHaveName,
    EmployeeAttributeMustHaveCode,
    UnauthorizedEditOfEmployeeAttribute
  }

  public enum JobTitleErrorCodes
  {
    JobTitleIdNotFound,
    JobTitleMustHaveUniqueName,
    JobTitleNameTooLong,
    JobTitleDescriptionTooLong,
    JobTitleNameIsRequired,
    JobTitleIsUsedInEmployeeJobAssignment
  }

  public enum AccrualRuleSetErrorCode
  {
    AccrualRuleSetIdNotFound,
    AccrualRuleSetMustHaveUniqueName,
    AccrualRuleSetNameTooLong,
    AccrualRuleSetNameIsRequired,
    AccrualRuleSetIsAssignedToEmployee,
    AccrualRuleNotFound,
    AdjustmentCategoryNotFound
  }

  public enum AccrualRuleSetErrorCodeArguments
  {
    AccrualRuleSetId
  }

  public enum PayAccumulatorError
  {
    NameMustBeUnique,
    NameTooLong,
    InvalidAccumulationTypeCode,
    InvalidAccumulationTypeAndResetCodeCombination,
    MissingDateTimeGroupAssignment,
    MissingHoursResetThreshold,
    CannotDeleteUsedPayAccumulators
  }
  public enum EmployeeHealthErrors
  {
    CommentExceeds255Characters,
    TemperatureIsNotValid,
    ReasonIsNotValid,
    ReasonNotBelongsToEmployeeHealthReasonType,
    EmployeeNotFound,
    EmployeeDoesNotBelongsToGivenSite,
    NoPayPeriodConfigurationForRequestedTimestamp
  }

  public enum SwapShiftErrors
  {
    FixedShiftCanNotBeSwapped,
    InvalidAction,
    InvalidActionNotTheRecipient,
    InvalidActionNotTheSender,
    InvalidSwapShiftRequestId,
    OriginalShiftDoesNotBelongToEmployee,
    OriginalShiftIsNotAValidShift,
    OriginalShiftIsNotInAvailableSwapWindow,
    RequestedShiftDoesNotBelongToEmployee,
    RequestedShiftIsNotAValidShift,
    RequestedShiftStateInvalid,
    ShiftHasBeenTaken,
    UnauthorizedUser,
    PendingEditExistsForScheduledShift
  }

  public enum FeatureConfigErrors
  {
    InvalidSite,
    InvalidSecurityGroup,
    OrgHierarchyIdCouldNotBeConvertedToInt,
    SecurityGroupIdCouldNotBeConvertedToInt
  }

  public enum EmployeeJobPrioritiesErrorCode
  {
    EmployeeIdNotFound,
    JobIdNotValid,
    JobIsNotCdmdToSite,
    EmployeeCannotRankPrimaryJob,
    PastJobsCannotBeRanked,
    CannotHaveDifferentRankForSameJob,
    CannotHaveSameRankForDifferentJobs,
    JobIdNotAssignedToEmployee,
    CannotUpdateOrAccessWhenEmployeePreferencesConfiguredToOff
  }

  public enum PunchEditApprovalErrors
  {
    InvaidPunchEditApproval,
    InvalidData,
    CommentOver255Characters,
    InvalidEmployee,
    InvalidStatusCode,
    InvalidApprovalCode,
    PunchEditApprovalIdDoesNotExist
  }

  public enum ShiftOfferErrorCodes
  {
    InvalidEmployee,
    ScheduledShiftDoesNotExist,
    ShiftHasAlreadyBeenOffered,
    ShiftIsNotOffered,
    EmployeeCannotOfferShift,
    EmployeeCannotCancelOfferedShift,
    FeatureIsNotConfiguredForEmployee,
    PendingEditExistsForScheduledShift
  }

  public enum TimeOffRequestErrors
  {
    CannotAddRequestForEmployeeBeforeHire,
    CannotAddRequestForInactiveEmployee,
    CannotChangeAllDayPartialDayWhenStartIsInClosedPayPeriodOrBeforeCurrentBusinessDate,
    CannotEditTimeOffRequestInAClosedPayPeriod,
    CannotEditTimeOffRequestWhichStartsAndEndsInAClosedPayPeriod,
    TimeOffRequestDatesInClosePayPeriodAreNotEditable,
    TimeOffRequestDatesCannotBeEditedToFallInClosedPayPeriod,
    CannotApproveTimeOffRequestWithoutTimeOffType,
    TimeOffRequestCannotOverlapExistingTimeOffRequestForMultipleDay,
    TimeOffRequestCannotOverlapExistingTimeOffRequestForSingleDay,
    TimeOffRequestCannotHaveAZeroLength,
    TimeOffRequestHalfDayHoursCannotBeUsedForMultiDayTimeOffRequests,
    TimeOffRequestLongerThanClientSettingForSelf,
    TimeOffRequestLongerThanClientSetting,
    TimeOffRequestLongerThan365Days,
    TimeOffBlackoutPeriodOverlapForSelf,
    TimeOffBlackoutPeriodOverlap,
    EmployeeAccrualBalanceViolationForSelf,
    EmployeeAccrualBalanceViolationInHours,
    EmployeeAccrualBalanceViolationInDays,
    MinTimeInAdvanceForSelf,
    CannotCancelOwnPastTimeOffRequest,
    CannotModifyOwnRequestNotInPendingStatus,
    CannotApproveOrModifyTorThatOverlapsTransferDate,
    TimeOffRequestDatesCannotBeEditedWhenItCreatedThroughCallOff,
    CannotAddOrEditTimeOffRequestInAClosedPayPeriod,
    TimeOffRequestRangeShouldBeWithinHomeSiteAssignmentRange,
    CannotUpdateTimeOffRequestInClosedPayPeriodWhenCurrentStatusNotEqualToApproved,
    CannotChangeApprovedTorStatusInClosePayPeriod,
    AuditReasonIsNotRequired,
    AuditReasonIsRequired,
    AuditReasonIsNotFound,
    AuditReasonIdShouldBeOfBackPayType,
    BackPayMappingsShouldBeProvidedForExistingAndChangedAdjustmentTypes,
    BackPayMappingsAreRequired,
    BackPayMappingsAreNotRequired,
    StartMustBeBeforeEnd,
    CommentExceeds255Characters,
    TimeOffRequestIsStale,
    TimeOffIdCouldNotConvertToLong,
    TimeOffTypeIdCouldNotBeConvertedToInt,
    TimeOffTypeNotAllowedForUser,
    EmployeeAccrualBalanceViolationOnUpdateInHours,
    EmployeeAccrualBalanceViolationOnUpdateInDays,
    CannotEditReadOnlyTimeOffRequest,
    CannotCancelReadOnlyTimeOffRequest,
    CannotCreateTimeOffRequestWithHiddenTimeOffType,
    CannotUpdateTimeOffRequestWithHiddenTimeOffType,
    TimeOffLimitExceededForJob
    }

  public enum TimeOffTypesErrorCodes
  {
    PrimaryJobNotAssigned
  }

  public enum SiteErrorCodes
  {
    SiteDoesNotExist
  }

  public enum MyAvailableShiftsErrorCodes
  {
    InvalidShift,
    EmployeeNotAutharizedToClaimTheShift,
    CannotWorkSplitShifts,
    CannotScheduleEmployeeWhoIsNeitherBorrowedNorOwned,
    ShiftNotAvailableToClaim,
    ShiftHasAlreadyBeenClaimed
  }

  public enum MyInfoErrorCodes
  {
    NotAuthorized,
    EmployeeIdNotProvided,
    InvalidEmployee,
    InvalidEmployeeFirstName,
    InvalidEmployeeMiddleName,
    InvalidEmployeeLastName,
    InvalidEmployeeSuffixName,
    InvalidEmployeeNickName,
    InvalidEmployeeCountryName,
    GivenEmployeeInformationisNotValid
  }

  public enum ReferenceDataErrorCodes
  {
    NotAuthorized,
    InvalidEmployee,
    InvalidSite,
    InvalidSecurityGroup
  }

  public enum PushMsgUserPreferenceErrorCodes
  {
    InvalidUser,
    InvalidTopicId,
    PreferenceDoExist,
    PreferenceDoesNotExist
  }

  public enum CalendarErrorCodes
  {
    GivenYearIsNotValid,
    GivenMonthIsNotValid
  }

  public enum ReasonErrorCodes
  {
    ReasonIdNotFound,
    ReasonIsNotCDMedToSite
  }

  public enum ReasonTypeErrorCodes
  {
    ReasonTypeIdNotFound
  }

  public enum MyCallOffErrorCodes
  {
    ScheduleShiftShouldBeValid,
    ReasonShouldBeCallOffType,
    ReasonIsNotValidOrCdmedToSite,
    ScheduledShiftBelongsOtherEmployee,
    EmployeeIsNotAllowedToCallOff,
    ShiftViolatesMaxHoursInAdvanceForCallOff,
    EmployeeAccrualBalanceViolationForSelf,
    EmployeeAccrualBalanceViolationInDays,
    EmployeeAccrualBalanceViolationInHours,
    TimeOffBlackoutPeriodOverlap,
    TimeOffRequestLongerThanClientSetting,
    ScheduleShiftIsNotPosted,
    PendingEditExistsForScheduledShift,
    TimeOffLimitExceededForCallOffTor
  }

  public enum PayAdjustmentErrors
  {
    ExceedsWarningThresholdAmount,
    ExceedsErrorThresholdAmount,
    ExceedsWarningThresholdHour,
    ExceedsErrorThresholdHour,
    ExceedsWarningThresholdQuantity,
    ExceedsErrorThresholdQuantity,
    AdjustmentMappedToExportedSpecialPay,
    AdjustmentMappedToSpecialPay
  }

  public enum VoluntaryTimeOffErrorCode
  {
    VoluntaryTimeOffIdInvalidOrNotFound,
    VoluntaryTimeOffInvalidAsScheduledShiftWasModified,
    VoluntaryTimeOffInvalidAsScheduledShiftHasStarted,
    VoluntaryTimeOffQuantitySentLimitReached,
    ValidAdjustmentCategoryRequired,
    VoluntaryTimeOffFeatureIsDisabled,
    VoluntaryTimeOffInvalidAsScheduledShiftHasPunches,
    VoluntaryTimeOffHasAlreadyBeenAccepted,
    VoluntaryTimeOffHasAlreadyBeenDeclined,
    VoluntaryTimeOffHasAlreadyBeenCancelled,
    VoluntaryTimeOffHasAlreadyBeenExpired,
    VoluntaryTimeOffHasAlreadyBeenRecalled
  }

  public enum CommonErrorCodes
  {
    StringCouldNotBeConvertedToInt
  }

  public enum EmployeeJobErrorCode
  {
    SiteDoesNotExist,
    JobDoesNotExist,
    EmployeeIsNotAssignedToSiteOnDate
  }

  public enum SitePunchSettingsErrorCodes
  {
    InvalidSite,
    EmployeeNotAssociatedToSite
  }

  public enum SitePunchSettingsErrors
  {
    SiteDoesNotExist
  }
  public enum RawPunchErrorCodes
  {
    InvalidSite,
    InvalidEmployee,
    InvalidDevice,
    InvalidBadgeNumber,
    InvalidJob,
    EmployeeNotActive,
    JobIdRequiredDuringJobTransfer,
    InvalidWorkedSite,
    ManagerAuthorizationRequired,
    CannotCreatePunchInFuture,
    CannotOverlapPreviousPunch,
    CannotCreatePunchPriorToPreviousCalendarDay,
    EmployeeHealthStatusDoesNotExist,
    EmployeeHealthWithPositiveStatusNotAllowedToPunchIn,
    InvalidRawPunchId
  }

  public enum SelfRawPunchErrorCodes
  {
    EmployeeSelfPunchCannotHaveManagerOverride,
    EmployeeSelfPunchTimeStampIsNotValid,
    AllowSelfPunchSiteSettingIsDisabled,
    InvalidScheduledShiftId,
    ScheduledShiftDoesNotBelongToEmployee
  }

  public enum MySitePayPeriodSummaryErrorCodes
  {
    NoOpenPayPeriod
  }

  public enum MyPunchEditsErrorCode
  {
    InvalidPunchedShiftId,
    InvalidMyPunchEditId,
    CommentOver255Characters,
    PunchedShiftDoesNotBelongToEmployee,
    CannotAddPunchedShiftStart,
    PayPeriodIsClosed,
    CurrentAndFutureDatedEditsAreNotAllowed,
    InvalidPunchId,
    CannotEditApprovedPunchOperation,
    CannotEditExpiredPunchOperation,
    CannotEditCancelledPunchOperation,
    PendingPunchEditRequestExistsForThePunchedShift,
    NoPendingEditsExistsToCancel,
    PunchAddsNotAllowed,
    PunchEditsNotAllowed,
    PunchAddEditsNotAllowed,
    MultiplePunchedShiftEndsNotAllowed,
    DetailsOutsidePunchedShift,
    JobTransferOutsidePunchedShift,
    StartMustBeBeforeEnd,
    CannotHaveZeroLengthPunchedShiftOrPunchDetails,
    DetailEndPunchMustBeAfterDetailStartPunch,
    JobTransferOverlapsDetail,
    DetailsOutOfOrder,
    JobTransfersOutOfOrder,
    ReasonCodeIsNotValid,
    PunchRejectedDuplicatePunch,
    PunchRejectedInvalidDaylightSavings,
    JobIdRequiredDuringJobTransfer,
    JobTransferAtShiftEnd,
    CannotModifyDayCutJobTransferStartTime,
    PunchRejectedShiftCannotExceed24Hours,
    EmployeeNotAllowedToPunch,
    DetailMustHaveStartTime,
    CannotWorkJobAssociatedWithPunch,
    JobRequiredForPunchedShiftStartEdit,
    CannotOverlapPreviousPunch,
    JobIsNotAllowedForRestrictedDepartment,
    PunchedShiftMappedToExportedSpecialPay,
    NoPayPeriodConfigurationForRequestedTimestamp,
    InvalidCalendarDate
  }

   public enum MyTimeCardErrorCodes
   {
    InvalidCalendarDate,
    NoPayPeriodConfigurationForRequestedTimestamp
  }

   public enum UserAuthorizationErrorCodes
   {
        LiamRealmServiceNotConfigured,
        LiamRealmServiceAuthenticationFailed,
        UnauthorizedAccess
   }

  public enum ContactInformationErrorCodes
  {
    ValueLengthIsMoreThanFieldLength
  }

  public enum EmployeeNotificationPreferencesError
  {
    HomePhoneNotProvided,
    CellPhoneNotProvided,
    CellPhoneNotProvidedForText,
    WorkPhoneNotProvided,
    EmailNotProvided
  }

  public enum ScheduledShiftEditErrorCodes
  {
    CannotModifyWhenScheduledShiftEditApprovalFeatureIsOff,
    CannotModifyScheduledShiftEditNotInPendingStatus,
    ScheduledShiftEditDoesNotBelongToEmployee,
    ScheduledShiftNotFound,
    ScheduledShiftDoesNotBelongToSite, 
    ScheduledShiftDoesNotContainsJob,
    ScheduledJobDoesNotExist,
    MinimumShiftQuantityShouldBeOne,
    CannotHaveActivityForUnfilledShifts,
    ScheduledShiftJobCannotOverlap,
    EmployeeIdNotFound,
    StartMustBeBeforeEnd,
    ShiftMustBeLongerThanAbsoluteMinimum,
    InvalidShiftActivityReason,
    UnsuportedDetailType,
    CannotAddOrModifyOrDeleteShiftInPastDays,
    CannotHaveZeroLengthDetail,
    CannotScheduleBelowMinHoursBetweenShifts,
    CannotScheduleDuringSchoolForMinor,
    CannotScheduleEarliestStartTimeForMinor,
    CannotScheduleEmployeeWhoIsNeitherBorrowedNorOwned,
    CannotScheduleLatestEndTimeForMinor,
    CannotScheduleOverMaxConsecutiveDaysAcrossWeeks,
    CannotScheduleOverMaxConsecutiveDaysAcrossWeeksForMinor,
    CannotScheduleOverMaxConsecutiveDaysInWeekForMinor,
    CannotScheduleOverMaxConsecutiveDaysPerWeek,
    CannotScheduleOverMaxDaysPerWeek,
    CannotScheduleOverMaxDaysPerWeekForMinor,
    ScheduledShiftCannotOverlapFullDayTimeOffRequest,
    ScheduledShiftCannotOverlapPartialTimeOffRequest,
    ShiftShouldHaveAtleastOneJob,
    ShiftShouldStartAfter24Hours,
    UnableToPlaceMealBreaksThroughAutoMealBreakGenerator,
    ScheduledShiftCannotOverlap,
    ReasonIsNotScheduleEditType,
    ReasonIsNotCDMedToSite,
    ReasonIdRequired,
    ReasonDoesNotExist,
    NoPayPeriodConfigurationForRequestedTimestamp,
    LaborRoleIdRequiredForRoleType,
    LaborRoleIdNotFound,
    JobIsNotCdmdToSite,
    JobIsNotAllowedForRestrictedDepartment,
    JobIdNotFound,
    EmployeeIsNotActive,
    EmployeeHasNoStatusForSearchDate,
    EmployeeCanNotWorkJob,
    DetailsOutsideScheduledShift,
    DetailsOutOfOrder,
    CannotWorkSplitShifts,
    CanNotWorkLaborRole,
    CannotScheduleOverMaxHoursPerWeekForMinor,
    CannotScheduleOverMaxHoursPerWeek,
    CannotScheduleOverMaxHoursPerDayForMinor,
    CannotDeleteShiftThatHasPunches,
    CannotScheduleOverMaxHoursPerDay,
    CannotUnfillShiftThatHasPunches
  }
}