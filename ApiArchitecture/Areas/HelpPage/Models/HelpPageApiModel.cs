using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using Jda.WfmEssApi.Common;
using Jda.WfmEssApi.Areas.HelpPage.ModelDescriptions;

namespace Jda.WfmEssApi.Areas.HelpPage.Models
{
    /// <summary>
    /// The model that represents an API displayed on the help page.
    /// </summary>
    public class HelpPageApiModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpPageApiModel"/> class.
        /// </summary>
        public HelpPageApiModel()
        {
            UriParameters = new Collection<ParameterDescription>();
            SampleRequests = new Dictionary<MediaTypeHeaderValue, object>();
            SampleResponses = new Dictionary<MediaTypeHeaderValue, object>();
            ErrorMessages = new Collection<string>();
        }

        /// <summary>
        /// Gets or sets the <see cref="ApiDescription"/> that describes the API.
        /// </summary>
        public ApiDescription ApiDescription { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ParameterDescription"/> collection that describes the URI parameters for the API.
        /// </summary>
        public Collection<ParameterDescription> UriParameters { get; private set; }

        /// <summary>
        /// Gets or sets the documentation for the request.
        /// </summary>
        public string RequestDocumentation { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ModelDescription"/> that describes the request body.
        /// </summary>
        public ModelDescription RequestModelDescription { get; set; }

        /// <summary>
        /// Gets the request body parameter descriptions.
        /// </summary>
        public IList<ParameterDescription> RequestBodyParameters
        {
            get { return GetParameterDescriptions(RequestModelDescription); }
        }

        /// <summary>
        /// Gets or sets the <see cref="ModelDescription"/> that describes the resource.
        /// </summary>
        public ModelDescription ResourceDescription { get; set; }

        /// <summary>
        /// Gets the resource property descriptions.
        /// </summary>
        public IList<ParameterDescription> ResourceProperties
        {
            get { return GetParameterDescriptions(ResourceDescription); }
        }

        /// <summary>
        /// Gets the sample requests associated with the API.
        /// </summary>
        public IDictionary<MediaTypeHeaderValue, object> SampleRequests { get; private set; }

        /// <summary>
        /// Gets the sample responses associated with the API.
        /// </summary>
        public IDictionary<MediaTypeHeaderValue, object> SampleResponses { get; private set; }

        public IEnumerable<IErrorResponse> ErrorResponses { get; set; }

        public string ExtraDocumentationViewName { get; set; }

        public string ExtraDocumentationViewNotFoundErrorMessage
        {
            get
            {
                return "Error: cannot display additional documentation for " + ExtraDocumentationViewName + ".cshtml";
            }
        }

        public static bool ShouldDocumentParameter(ApiParameterDescription parameter)
        {
            var isNotCancellationToken =
                !typeof (System.Threading.CancellationToken).IsAssignableFrom(
                    parameter.ParameterDescriptor.ParameterType);
            var isNotFromRequestBody = parameter.Source != ApiParameterSource.FromBody;
            return isNotCancellationToken && isNotFromRequestBody;
        }

        // start
        public static ILookup<HttpControllerDescriptor, ApiDescription> GetAPIsToDocument(
            Collection<ApiDescription> model)
        {
            // Pick best version of each API, and group them by controller
            var apisGroupedByActualFunction = GroupDifferentFlavorsOfSameApiTogether(model);
            var desiredApis = apisGroupedByActualFunction.SelectMany(x => PickBestVersionsOfApiFromGroup(x.Value));
            var apisCustomization = desiredApis.Select(CustomizeApiDisplay);
            var apisGroupedByController =
                apisCustomization.ToLookup(a => a.ActionDescriptor.ControllerDescriptor);
            var apisToDocument = apisGroupedByController.ToDictionary(g => g.Key, g => PickDisplayOrder(g).ToList());
            return ConvertDictToLookup(apisToDocument);
        }

        private static Dictionary<string, List<ApiDescription>> GroupDifferentFlavorsOfSameApiTogether(
            IEnumerable<ApiDescription> model)
        {
            return model.ToLookup(a => a.HttpMethod + " " + a.Documentation).ToDictionary(x => x.Key, x => x.ToList());
        }

        private static IEnumerable<ApiDescription> PickBestVersionsOfApiFromGroup(IEnumerable<ApiDescription> apis)
        {
            var reorderedApis = PickRelativePathsWithLeastNumberOfParameterAssignments(apis);
            return reorderedApis.Take(1);
        }

        private static IEnumerable<ApiDescription> PickRelativePathsWithLeastNumberOfParameterAssignments(
            IEnumerable<ApiDescription> apis)
        {
            return apis.OrderBy(a => a.RelativePath.Count(p => p.Equals('='))); // choose /{id}/ over ?id={id}
        }

        private static IEnumerable<ApiDescription> PickDisplayOrder(IEnumerable<ApiDescription> apis)
        {
            return apis.OrderBy(d => d.HttpMethod.ToString());
        }

        private static ApiDescription CustomizeApiDisplay(ApiDescription api)
        {
            CustomizeApiDisplayForMultipleIds(api); // Append ?ids={ids}&ids={ids} if necessary
            return api;
        }

        private static void CustomizeApiDisplayForMultipleIds(ApiDescription api)
        {
            foreach (var param in api.ParameterDescriptions)
            {
                var parameterName = param.Name;
                var hasParameterNamedIds = parameterName.EndsWith("Ids");
                var relativePathDoesNotAlreadyIncludeParameter = !api.RelativePath.Contains("Ids");
                if (hasParameterNamedIds && relativePathDoesNotAlreadyIncludeParameter)
                {
                    var combinationString = api.RelativePath.Contains("?") ? "&" : "?";
                    api.RelativePath += combinationString + parameterName + "={ids}&" + parameterName + "={ids}";
                }
            }
        }

        private static ILookup<HttpControllerDescriptor, ApiDescription> ConvertDictToLookup(
            IDictionary<HttpControllerDescriptor, List<ApiDescription>> dictionary)
        {
            return dictionary.SelectMany(p => p.Value.Select(x => new {p.Key, Value = x}))
                .ToLookup(pair => pair.Key, pair => pair.Value);
        }

        //end

        /// <summary>
        /// Gets the error messages associated with this model.
        /// </summary>
        public Collection<string> ErrorMessages { get; private set; }

        private static IList<ParameterDescription> GetParameterDescriptions(ModelDescription modelDescription)
        {
            ComplexTypeModelDescription complexTypeModelDescription = modelDescription as ComplexTypeModelDescription;
            if (complexTypeModelDescription != null)
            {
                return complexTypeModelDescription.Properties;
            }

            CollectionModelDescription collectionModelDescription = modelDescription as CollectionModelDescription;
            if (collectionModelDescription != null)
            {
                complexTypeModelDescription =
                    collectionModelDescription.ElementDescription as ComplexTypeModelDescription;
                if (complexTypeModelDescription != null)
                {
                    return complexTypeModelDescription.Properties;
                }
            }

            return null;
        }
    }
}