using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;
using DHHelper.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DHHelper.Helper
{

    public static class HttpHelper
    {

        public static string ConvertToQueryString(this RequestBase obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();

            List<string> queryStringValues = new List<string>();

            foreach (var property in properties)
            {

                bool IsIgnore = Attribute.IsDefined(property, typeof(JsonIgnoreAttribute));

                if (IsIgnore)
                {
                    continue;
                }

                object? propertyValue = property.GetValue(obj);

                if (propertyValue == null)
                {
                    continue;
                }


                bool hasJsonProperty = Attribute.IsDefined(property, typeof(JsonPropertyAttribute));

                string key = "";
                string value = "";

                if (hasJsonProperty)
                {

                    JsonPropertyAttribute? jsonProperty = property.GetCustomAttribute<JsonPropertyAttribute>(false);

                    if (jsonProperty != null && !string.IsNullOrWhiteSpace(jsonProperty.PropertyName))
                    {

                        key = jsonProperty.PropertyName!;
                    }
                    else
                    {
                        key = property.Name;
                    }

                }
                else
                {
                    key = property.Name;
                }

                if (propertyValue != null)
                {

                    EnumDataTypeAttribute? enumDataType = property.GetCustomAttribute<EnumDataTypeAttribute>(false);

                    if (enumDataType != null)
                    {
                        
                        MemberInfo? enumMember = enumDataType.EnumType.GetMember(propertyValue.ToString()!).FirstOrDefault();

                        if (enumMember != null)
                        {

                            EnumMemberAttribute? em = (EnumMemberAttribute?)enumMember.GetCustomAttributes(typeof(EnumMemberAttribute), false).FirstOrDefault();

                            if (em != null)
                            {
                                value = em.Value!;
                            }
                            else
                            {
                                value = propertyValue.ToString()!;
                            }

                        }

                    }else{

                        //json convert를 기반으로 하는게 편하려나 ..
                        string json = JsonConvert.SerializeObject(obj);

                        var jsonObject = JObject.Parse(json);

                        if(jsonObject != null){

                            value = jsonObject[key]!.ToString();
                        }else{
                            value = propertyValue.ToString()!;
                        }
                        

                        
                    }

                }

                queryStringValues.Add($"{key}={value}");

                // if (has_jsonproperty)
                // {
                //     JsonPropertyAttribute? json_property = property.GetCustomAttribute<JsonPropertyAttribute>(false);

                //     EnumDataTypeAttribute? enumDataType = property.GetCustomAttribute<EnumDataTypeAttribute>(false);

                //     if (json_property != null)
                //     {

                //         if (property.PropertyType.IsEnum)
                //         {

                //             MemberInfo? enumMember = property.GetType().GetMember(property.Name).FirstOrDefault();

                //             if (enumMember != null)
                //             {

                //                 EnumMemberAttribute? em = (EnumMemberAttribute?)enumMember.GetCustomAttributes(typeof(EnumMemberAttribute), false).FirstOrDefault();

                //                 if (em != null)
                //                 {
                //                     query_strings.Add($"{json_property.PropertyName}={em.Value}");
                //                 }
                //                 else
                //                 {

                //                     query_strings.Add($"{json_property.PropertyName}={property_value}");
                //                 }


                //             }

                //         }

                //         if (enumDataType != null)
                //         {

                //             var enum_member_attribute = AttributeHelper.GetAttribute<EnumMemberAttribute>(property_value);

                //             if (enum_member_attribute != null)
                //             {
                //                 query_strings.Add($"{json_property.PropertyName}={enum_member_attribute.Value}");
                //             }
                //             else
                //             {
                //                 query_strings.Add($"{json_property.PropertyName}={property_value}");
                //             }

                //         }
                //         else
                //         {
                //             query_strings.Add($"{json_property.PropertyName}={property_value}");
                //         }
                //     }
                //     else
                //     {

                //         query_strings.Add($"{property.Name}={property_value}");
                //     }

                // }
                // else if (!string.IsNullOrEmpty(property_value.ToString()))
                // {
                //     query_strings.Add($"{property.Name}={property_value}");
                // }

            }

            string queryString = string.Join("&", queryStringValues);

            return queryString;
        }

    }


}