using DHHelper.Helper;
using DHHelper.Models.Base;
using Newtonsoft.Json;

namespace DHSDK.Models.DTO.DataGo
{

    public class RequestCovid19 : RequestBase
    {
        public RequestCovid19(string secretKey) : base(secretKey)
        {
        }

        public override string EndPoint => "/openapi/service/rest/Covid19/getCovid19SidoInfStateJson";

        public override HttpMethod HttpMethod => HttpMethod.Get;

        [JsonRequired]
        public int PageNo { get; set; }

        [JsonRequired]
        public int NumOfRows{get; set;}

        [JsonRequired]
        [JsonConverter(typeof(DateFormatConverter), "yyyyMMdd")]
        public DateTime StartCreateDt{get ;set;}

        [JsonRequired]
        [JsonConverter(typeof(DateFormatConverter), "yyyyMMdd")]
        public DateTime EndCreateDt{get ;set;}

    }

}