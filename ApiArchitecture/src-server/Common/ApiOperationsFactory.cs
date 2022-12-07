namespace Jda.WfmEssApi.Common
{
  public abstract class ApiOperationsFactory<T> where T: class, new()
  {
    private static T _Instance;

    public static T GetInstance()
    {
      _Instance = _Instance ?? (_Instance = new T());
      return _Instance;
    }

    public static T GetInstance(
      T injectedFactory) //DI point
    {
      return _Instance = injectedFactory;
    }
  }
}