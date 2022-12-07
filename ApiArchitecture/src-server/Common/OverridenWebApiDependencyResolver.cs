using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Jda.WfmEssApi.Common
{
  // Found this simplified IoC for Controller Dependency Resolution
  // http://stackoverflow.com/questions/20752485/how-is-the-web-api-controllers-constructor-called 
  // no new 'magic' here, like Unity as Microsoft uses in their online examples for Dependency Resolver Overrides.
  // http://www.asp.net/web-api/overview/advanced/dependency-injection
  public class OverriddenWebApiDependencyResolver : WebApiOverrideDependency<IDependencyResolver>, IDependencyResolver
  {
    public OverriddenWebApiDependencyResolver Add(Type serviceType, Func<object> initializer)
    {
      Provided.Add(serviceType, initializer);
      return this;
    }

    public IDependencyScope BeginScope()
    {
      return new Scope(Inner.BeginScope(), Provided);
    }
    public OverriddenWebApiDependencyResolver(IDependencyResolver inner) : base(inner, new Dictionary<Type, Func<object>>()) { }
    public class Scope : WebApiOverrideDependency<IDependencyScope>, IDependencyScope
    {
      public Scope(IDependencyScope inner, IDictionary<Type, Func<object>> provided) : base(inner, provided) { }
    }
  }

  public abstract class WebApiOverrideDependency<T> : IDependencyScope where T : IDependencyScope
  {
    public void Dispose() => Inner.Dispose();
    public Object GetService(Type serviceType)
    {
      Func<Object> res;
      return Provided.TryGetValue(serviceType, out res) ? res() : Inner.GetService(serviceType);
    }

    public IEnumerable<Object> GetServices(Type serviceType)
    {
      Func<Object> res;
      return Inner.GetServices(serviceType).Concat(Provided.TryGetValue(serviceType, out res) ? 
                                                                        new[] { res() } : Enumerable.Empty<object>());
    }
    protected readonly T Inner;
    protected readonly IDictionary<Type, Func<object>> Provided;

    protected WebApiOverrideDependency(T inner, IDictionary<Type, Func<object>> provided)
    {
      this.Inner = inner;
      this.Provided = provided;
    }
  }
}