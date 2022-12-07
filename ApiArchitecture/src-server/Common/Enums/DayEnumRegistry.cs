namespace Jda.WfmEssApi.Common.Enums
{
  public class DayEnumRegistry : EnumRegistry<System.DayOfWeek, Day>
  {
    protected override void RegisterApiEnums()
    {
      RegisterEnumMapping(System.DayOfWeek.Sunday, Day.Sunday);
      RegisterEnumMapping(System.DayOfWeek.Monday, Day.Monday);
      RegisterEnumMapping(System.DayOfWeek.Tuesday, Day.Tuesday);
      RegisterEnumMapping(System.DayOfWeek.Wednesday, Day.Wednesday);
      RegisterEnumMapping(System.DayOfWeek.Thursday, Day.Thursday);
      RegisterEnumMapping(System.DayOfWeek.Friday, Day.Friday);
      RegisterEnumMapping(System.DayOfWeek.Saturday, Day.Saturday);
    }
  }
}