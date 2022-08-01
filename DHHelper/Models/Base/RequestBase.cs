using Newtonsoft.Json;
using DHHelper.Helper;
using DHHelper.Interfaces;

namespace DHHelper.Models.Base
{

    public class RequestBase : IRequestBase
    {
        [JsonIgnore]
        public virtual string EndPoint => throw new NotImplementedException();

        [JsonIgnore]
        public virtual HttpMethod HttpMethod => throw new NotImplementedException();

        [JsonIgnore]
        public virtual bool HasAuthorize => false;

        public virtual string ToPayload()
        {

            if (HttpMethod != null && HttpMethod != HttpMethod.Get)
            {
                string payload = JsonConvert.SerializeObject(this);

                return payload;
            }
            else
            {
                throw new Exception("Method must not Get");
            }

        }

        public virtual string ToQueryString()
        {
            if (HttpMethod != null && HttpMethod == HttpMethod.Get)
            {
                string querystring = this.ConvertToQueryString();

                return querystring;
            }
            else
            {
                throw new Exception("Method must Get");
            }

        }


    }

}