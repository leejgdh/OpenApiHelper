namespace DHHelper.Options
{

    public class ApiClientOption
    {
        public ApiClientOption(Uri host)
        {
            Host = host;
        }

        public Uri Host { get; set; }
    }

}