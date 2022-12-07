using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Validation.Providers;
using System.Web.Mvc;
using FluentNHibernate.Mapping;
using log4RP;




namespace Jda.WfmEssApi
{
  public class WebApiApplication : HttpApplication
  {
    protected DiagnosticExceptionType RequestedExceptionType;
    protected void Application_Start()
    {
      ExceptionOnAppStartUpIfNeeded();
      GlobalConfiguration.Configure(WebApiConfig.Register);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      WebApiConfigurator.Configure(GlobalConfiguration.Configuration);
      AreaRegistration.RegisterAllAreas();
      LoadMapAssembliesOutsideBaseDomain();
      GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
      GlobalConfiguration.Configuration.Filters.Add(new ModelValidationFilterAttribute());
      GlobalConfiguration.Configuration.MessageHandlers.Add(new RequestDiagnosticHandler());

      GlobalConfiguration.Configuration.Services.RemoveAll(
        typeof(System.Web.Http.Validation.ModelValidatorProvider),
        v => v is InvalidModelValidatorProvider);

      InitializeDomainModel();
    }

    protected void Application_PostAuthorizeRequest()
    {
      ThrowExceptionIfDiagnosticExceptionTypeMatchesRequest(
        DiagnosticExceptionType.ExceptionBeforeUnitOfWorkStart,
        "Exception thrown before Unit Of Work start.");

      WebApiConfigurator.TellAspToCallCustomSessionStore();
    }

    private static void LoadMapAssembliesOutsideBaseDomain()
    {
      BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(a => DomainModelHelper.IsMapAssembly(a)).ToList();
    }

    protected void Application_PostRequestHandlerExecute(object sender, EventArgs e)
    {
      ThrowExceptionIfDiagnosticExceptionTypeMatchesRequest(
        DiagnosticExceptionType.ExceptionInPostRequestHandler,
        "Exception thrown from Post-Request Handler.");
    }

    private const string AsmxWebRequestEnding = ".asmx";

    protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
    {
      ThrowExceptionIfDiagnosticExceptionTypeMatchesRequest(
        DiagnosticExceptionType.ExceptionInPreRequestHandler,
        "Exception thrown from Pre-Request Handler.");

      var oldAsmxWebServiceCall = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath != null &&
        HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.EndsWith(
          AsmxWebRequestEnding, StringComparison.OrdinalIgnoreCase);
      if (oldAsmxWebServiceCall)
      {
        SessionHelper.CreateUnitOfWorkForEssApi();
      }
    }

    protected void ThrowExceptionIfDiagnosticExceptionTypeMatchesRequest(
      DiagnosticExceptionType diagnosticExceptionType, string errorMessage)
    {
      var diagnosticString = Request.QueryString["runDiagnostic"];
      var nonDefaultExceptionRequested = Enum.TryParse(diagnosticString, out RequestedExceptionType);
      var diagnosticExceptionRequested = diagnosticExceptionType.Equals(RequestedExceptionType)
        && nonDefaultExceptionRequested;

      if (diagnosticExceptionRequested)
        throw new Exception(errorMessage);
    }

    protected static void ExceptionOnAppStartUpIfNeeded()
    {
      //Not working as intended? Make sure you go into the Web.config file and
      //uncomment the key-value pair that has the value 'diagnostics_throwException'
      //in appSettings

      var exceptionFoundInWebConfig = ConfigurationManager.AppSettings["diagnostics_applicationStart"];
      var exceptionRequestedInAppStartUp = "diagnostics_throwException".Equals(exceptionFoundInWebConfig);

      if (exceptionRequestedInAppStartUp)
        throw new Exception("Exception thrown from Application StartUp");
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
      HttpCookie sundialCookie = Request.Cookies["RPWeb-Sundial"];

      if (sundialCookie != null)
      {
        Sundial.Now = DateTime.Parse(sundialCookie.Value, CultureInfo.InvariantCulture);
      }
    }

    protected void Application_EndRequest(object sender, EventArgs e)
    {
      UnitOfWorkFactory.Dispose();
    }

    protected void Application_Error(object sender, EventArgs e)
    {
      //Server.Transfer("ApplicationError.aspx"); need to discuss
    }

    protected static void InitializeDomainModel()
    {
      var thread = new Thread(DomainModelHelper.Initialize);
      thread.Start();
    }

    private static bool AssemblyContainsClassMap(Assembly assembly)
    {
      bool exists = assembly.GetExportedTypes().Any(t => IsClassMap(t));

      return exists;
    }

    private static bool IsClassMap(Type type)
    {
      return type.BaseType != null && type.BaseType.IsGenericType &&
        type.BaseType.GetGenericTypeDefinition() == typeof(ClassMap<>);
    }
  }
}