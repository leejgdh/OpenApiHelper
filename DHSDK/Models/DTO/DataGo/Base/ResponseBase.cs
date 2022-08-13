namespace DHSDK.Models.DTO.DataGo
{

    public class ResponseBase<T> : DHHelper.Models.Base.ResponseBase<T>
    {
        public override string Root => "body";

    }
}