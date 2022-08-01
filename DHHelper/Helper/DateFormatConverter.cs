using Newtonsoft.Json.Converters;

namespace DHHelper.Helper
{

    public class DateFormatConverter : IsoDateTimeConverter
    {

        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }

    }

}