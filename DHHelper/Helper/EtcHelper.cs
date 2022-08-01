using System.Reflection;
using System.Runtime.Serialization;
using DHHelper.Models.Base;

namespace DHHelper.Helper
{
    public static class EtcHelper
    {
        /// <summary>
        /// 1단 배열 List를 지정한 갯수만큼 잘라서 2단으로 만들기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static List<List<T>> SplitList<T>(this List<T> array, int Count)
        {

            List<List<T>> result = new List<List<T>>();

            if (array.Count() > Count)
            {

                result.Add(array.GetRange(0, Count));
                result.AddRange(SplitList(array.GetRange(Count, array.Count() - Count), Count));
            }
            else
            {
                result.Add(array);
            }

            return result;
        }

        public static List<T> MergeList<T>(this List<List<T>> array)
        {

            List<T> response = new List<T>();

            foreach (var items in array)
            {
                response.AddRange(items);
            }

            return response;
        }


        public static List<T> MergeList<T>(this IEnumerable<IEnumerable<T>> array)
        {

            List<T> response = new List<T>();

            foreach (var items in array)
            {
                response.AddRange(items);
            }

            return response;
        }

        /// <summary>
        /// key 값으로 Distinct 하기
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        public static string ParseHttpParamFromObject(object obj)
        {
            var properties = obj.GetType().GetProperties();

            var param = "";
            foreach (var property in properties)
            {
                if (property.GetValue(obj) != null)
                {
                    param += "&" + property.Name + "=" + property.GetValue(obj);
                }
            }

            if (param.Length > 1)
            {
                param = param.Substring(1);
            }


            return param;
        }

        /// <summary>
        /// 날짜를 지정한 일수로 나누기
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="divide"></param>
        /// <returns></returns>
        public static List<DateRange> DivideDateRange(DateTime startDate, DateTime endDate, int divide)
        {
            //시간 00시로 맞춘다.

            //날짜 차이 구한다
            double total_day_double = (endDate - startDate).TotalDays;


            List<DateRange> range = new List<DateRange>();

            DateTime temp_start_date = startDate;
            for (int i = 0; i < total_day_double; i++)
            {

                DateTime temp_end_date = temp_start_date.AddDays(divide);

                if (temp_end_date <= endDate)
                {
                    range.Add(new DateRange
                    {
                        StartDate = temp_start_date,
                        EndDate = temp_end_date
                    });
                    temp_start_date = temp_end_date;
                }
                else
                {
                    if (temp_start_date == endDate)
                    {
                        break;
                    }
                    else
                    {
                        range.Add(new DateRange
                        {
                            StartDate = temp_start_date,
                            EndDate = endDate
                        });
                    }

                    break;
                }

            }
            return range;
        }

        public static DateTime ChangeTime(DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }


        /// <summary>
        /// Enum 배열을 Comma로 구분된 string으로 변경, 
        /// EnumMember 설정시 해당 값으로 변환
        /// </summary>
        /// <param name="enums">Enum 배열</param>
        /// <typeparam name="T">Enum 제한</typeparam>
        /// <returns></returns>
        public static string GetCommaString<T>(T[] enums) where T : Enum
        {
            List<string> result = new List<string>();

            foreach (var value in enums)
            {
                var enum_attr = AttributeHelper.GetAttribute<EnumMemberAttribute>(value);

                if (enum_attr != null)
                {
                    result.Add(enum_attr.Value!);
                }
                else
                {
                    result.Add(value.ToString());
                }

            }

            return string.Join(",", result);
        }


        public static string? GetQueryValue(Uri uri, string key)
        {
            var query_splited = uri.Query.Replace("?", string.Empty).Split('&');
            Dictionary<string, string> result = new Dictionary<string, string>();


            foreach (var query in query_splited)
            {
                var key_value = query.Split('=');

                if (key_value.Length == 2)
                {
                    result.Add(key_value[0], key_value[1]);
                }

            }

            result.TryGetValue(key, out string? value);

            return value;
        }

    }

}