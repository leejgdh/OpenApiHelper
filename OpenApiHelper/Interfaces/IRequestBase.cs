namespace OpenApiHelper.Interfaces
{

    public interface IRequestBase
    {
        string EndPoint { get; }

        HttpMethod HttpMethod { get; }

        string ToQueryString();

        string ToPayload();
    }

}