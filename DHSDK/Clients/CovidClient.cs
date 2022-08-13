
using DHHelper.Clients;
using DHHelper.Models.Base;
using DHHelper.Options;
using Microsoft.Extensions.Options;

namespace DHSDK.Clients
{

    public class CovidClient : ApiClient<RequestBase>
    {
        public CovidClient(
            IOptionsSnapshot<ApiClientOption> apiClientOption,
            IHttpClientFactory clientFactory) : base(apiClientOption, clientFactory)
        {
        }
    }

}