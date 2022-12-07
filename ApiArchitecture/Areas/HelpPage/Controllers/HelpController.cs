using System;
using System.Web.Http;
using System.Web.Mvc;
using Jda.WfmEssApi.Areas.HelpPage;
using Jda.WfmEssApi.Areas.HelpPage.ModelDescriptions;
using Jda.WfmEssApi.Areas.HelpPage.Models;

namespace Jda.WfmEssApi.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Api(string apiId)
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            var showAllApiRoutes = String.IsNullOrEmpty(apiId);
            if (showAllApiRoutes)
            {
                return View("ApiIndex", Configuration.Services.GetApiExplorer().ApiDescriptions);
            }

            HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
            var apiHelpModelExists = apiModel != null;
            if (apiHelpModelExists)
            {
                return View("Api", apiModel);
            }

            return View("Error");
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}