using System.Xml.Serialization;

namespace DHSDK.Models.DTO.DataGo
{

    // [System.Xml.Serialization.XmlRootAttribute("response", Namespace = "", IsNullable = false)]
    [XmlRoot("response")]
    public struct ResponseCovid19
    {
        [XmlElement("body")]
        public _Body Body { get; set; }

        public class _Body
        {

            [XmlArray("items")]
            [XmlArrayItem("item")]
            public List<_Item> Items { get; set; }

            public struct _Item
            {
                public string ResultCode { get; set; }

                public string resultMsg { get; set; }

                public int NumOfRows { get; set; }

                public int PageNo { get; set; }

                public int TotalCount { get; set; }

                public string SEQ { get; set; }

                public DateTime CreateDt { get; set; }

                public int DeathCnt { get; set; }

                [XmlElement("gubun")]
                public string Gubun { get; set; }

                public string GubunCn { get; set; }

                public string GubunEn { get; set; }

                public int IncDec { get; set; }

                public int IsolClearCnt { get; set; }

                public double QurRate { get; set; }

                public string StdDay { get; set; }

                public DateTime? UpdateDt { get; set; }

                public int DefCnt { get; set; }

                public int OverFlowCnt { get; set; }

                public int LocalOccCnt { get; set; }

            }


        }


    }

}