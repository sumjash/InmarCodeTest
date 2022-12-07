using RPCore;

namespace Jda.WfmEssApi.Helpers
{
  public static class FreeTextHelper
  {
    public static FreeText ConvertToFreeText(string original)
    {
      var freeText = new FreeText(original);
      freeText.Validate();
      return freeText;
    }
    public static string GetValidatedFreeTextValue(FreeText value)
    {
      var valueExist = FreeText.IsNullOrEmpty(value);
      if (valueExist)
      {
        return string.Empty;
      }
      value.RemoveInvalidCharactersForFreeText();
      return FreeText.GetValidatedTextWithoutEncoding(value).Trim();
    }
  }
}