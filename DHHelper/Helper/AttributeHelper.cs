using System.Reflection;

namespace DHHelper.Helper
{
    public static class AttributeHelper
    {
        public static T? GetAttribute<T>(Enum enumValue) where T : Attribute
        {
            T? attribute;

            MemberInfo? memberInfo = enumValue.GetType().GetMember(enumValue.ToString())
                                            .FirstOrDefault();

            if (memberInfo != null)
            {
                attribute = (T?)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                return attribute;
            }

            return null;
        }

        public static T? GetAttribute<T>(object obj) where T : Attribute
        {

            var type = obj.GetType();

            MemberInfo? memberInfo = obj.GetType().GetMember(nameof(obj))
                                            .FirstOrDefault();

            if (memberInfo != null)
            {
                var attribute = (T?)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();

                return attribute;
            }

            return null;
        }

    }


}