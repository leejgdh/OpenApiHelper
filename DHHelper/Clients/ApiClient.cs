using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using DHHelper.Interfaces;
using DHHelper.Models.Base;
using DHHelper.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DHHelper.Clients
{

    public class ApiClient<T> where T : IRequestBase
    {
        public HttpClient _requestClient;

        public ApiClientOption _apiClientOption;

        public ApiClient(
            IOptionsSnapshot<ApiClientOption> apiClientOption,
            IHttpClientFactory clientFactory
        )
        {
            _requestClient = clientFactory.CreateClient();

            _apiClientOption = apiClientOption.Value;

            _requestClient.BaseAddress = _apiClientOption.Host;

        }

        public virtual async Task<TaskBase<R?>> SendRequestAsync<R>(IRequestBase request)
        {
            TaskBase<R> result = new TaskBase<R>();

            try
            {
                SetHeader();

                var httpResponse = await SendRequest(request);

                result.IsSuccess = httpResponse.IsSuccessStatusCode;

                string content = await httpResponse.Content.ReadAsStringAsync();

                if (result.IsSuccess)
                {
                    if (request.ResponseType == "Xml")
                    {

                        XmlSerializer ser = new XmlSerializer(typeof(R));

                        using (TextReader reader = new StringReader(content))
                        {
                            result.Result = (R?)ser.Deserialize(reader);
                        }
                    }
                    else if (request.ResponseType == "Json")
                    {
                        result.Result = JsonConvert.DeserializeObject<R>(content);
                    }

                }
                else
                {

                    result.Message = content;

                }

            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Message = $"System Error {e.Message}";
            }


            return result;
        }

        private Task<HttpResponseMessage> SendRequest(IRequestBase request)
        {

            if (request.HttpMethod == HttpMethod.Get)
            {

                string endPoint = $"{request.EndPoint}?{request.ToQueryString()}";

                HttpRequestMessage message = new HttpRequestMessage(request.HttpMethod, endPoint);

                return _requestClient.SendAsync(message);

            }
            else
            {

                HttpRequestMessage message = new HttpRequestMessage(request.HttpMethod, request.EndPoint);

                message.Content = new StringContent(request.ToPayload(), Encoding.UTF8, "application/json");

                return _requestClient.SendAsync(message);
            }

        }

        public virtual void SetHeader() { }
    }

}