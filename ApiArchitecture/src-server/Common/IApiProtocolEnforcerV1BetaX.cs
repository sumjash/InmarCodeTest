using System;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Jda.WfmEssApi.Common
{
  public interface IApiProtocolEnforcerV1BetaX
  {
    HttpRequestMessage Request { get; }

    void SetUrlHelper(UrlHelper urlHelper);
    void SetRequest(HttpRequestMessage request);
    //CRUD Operations
    HttpResponseMessage Create(ISingleResourceCommandOperationCoordinator commandOperationCoordinator);
    HttpResponseMessage Read(ISingleResourceApiQuery query);
    HttpResponseMessage Read(ICollectionResourceApiQuery query);
    HttpResponseMessage Read(ISingleResourceQueryOperationCoordinator queryOperationCoordinator);
    HttpResponseMessage Read(ICollectionResourceQueryOperationCoordinator queryOperationCoordinator);
    HttpResponseMessage Update(ISingleResourceCommandOperationCoordinator commandOperationCoordinator);
    HttpResponseMessage Delete(ISingleResourceCommandOperationCoordinator commandOperationCoordinator);
    //Additional Operations
    HttpResponseMessage CreateMalformedRequestResponse(Type requestResourceType, string parameterName = null);
    HttpResponseMessage CreateResponseForWhenUriIdDoesNotMatchResourceId(string idOnResource, string idInUrl, string parameterName);
    HttpResponseMessage CreateResponseForWhenUriIdDoesNotMatchResourceId(long idOnResource, long idInUrl, string parameterName);
    HttpResponseMessage CreateResponseForWhenUriIdDoesNotMatchResourceId(int idOnResource, int idInUrl, string parameterName);
  }
}