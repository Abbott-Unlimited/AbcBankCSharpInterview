using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace abc_bank
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {            
            Type type = e.GetType();
            Array values = Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var descriptionAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                    {
                        return descriptionAttribute.Description;
                    }
                }
            }

            return string.Empty;
        }
    }
}