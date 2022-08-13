using Newtonsoft.Json;

namespace DHSDK.Models.DTO.DataGo
{

    public class RequestBase : DHHelper.Models.Base.RequestBase
    {
        public RequestBase(string serviceKey)
        {

            ServiceKey = serviceKey;
        }

        [JsonRequired]
        public string ServiceKey { get; set; }

        [JsonIgnore]
        public override string ResponseType => "Xml";

    }
}