using DHHelper.Clients;
using DHHelper.Options;
using Microsoft.Extensions.Options;
using ShopeeSDK.Models.Base;

namespace ShopeeSDK.Clients
{

    public class ShopeeClient : ApiClient<RequestBase>
    {
        public ShopeeClient(
            IOptionsSnapshot<ApiClientOption> apiClientOption,
            IHttpClientFactory clientFactory) : base(apiClientOption, clientFactory)
        {

        }
    }



}