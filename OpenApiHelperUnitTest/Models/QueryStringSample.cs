using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using DHHelper.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OpenApiHelperUnitTest.Models
{
    public class SampleModel : RequestBase
    {
        public override string EndPoint => "/api/Test";

        public override HttpMethod HttpMethod => HttpMethod.Get;

        public override bool HasAuthorize => base.HasAuthorize;

        [JsonProperty("name")]
        [JsonRequired]
        public string? Name { get; set; }

        [JsonRequired]
        public int? Qty { get; set; }

        public List<int>? Ids { get; set; }

        [EnumDataType(typeof(EStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("status")]
        public EStatus? Status{get; set;}


        [EnumDataType(typeof(EStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Status")]
        public EStatus? Status2{get; set;}

    }


    public enum EStatus
    {
        [EnumMember(Value = "normal")]
        NORMAL,

        [EnumMember(Value = "BLOCKED")]
        BLOCKED



    }

}