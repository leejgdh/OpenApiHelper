namespace DHHelper.Interfaces
{

    public interface IRequestBase
    {
        string EndPoint { get; }

        HttpMethod HttpMethod { get; }

        string ResponseType { get; set; }

        string ToQueryString();

        string ToPayload();
    }

}