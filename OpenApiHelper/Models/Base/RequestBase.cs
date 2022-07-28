using Newtonsoft.Json;
using OpenApiHelper.Interfaces;

namespace OpenApiHelper.Models.Base
{

    public class RequestBase : IRequestBase
    {
        public virtual string EndPoint => throw new NotImplementedException();
        
        public virtual HttpMethod HttpMethod => throw new NotImplementedException();
        
        public virtual string ToPayload()
        {

            string payload = JsonConvert.SerializeObject(this);

            return payload;
        }
        
        public virtual string ToQueryString()
        {
            string payloadJson = ToPayload();

            

            string querystring = "";


            return querystring;
        }


    }

}