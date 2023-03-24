using System.ComponentModel;
using System;
using System.Linq;
using System.Reflection;

namespace abc_bank
{
	public static class ExtensionMethods
	{
		public static Expected GetAttributeValue<T, Expected>(this Enum enumeration, Func<T, Expected> expression) where T : Attribute
		{
			T attribute = enumeration
				.GetType()
				.GetMember(enumeration.ToString())
				.Where(member => member.MemberType == MemberTypes.Field)
				.FirstOrDefault()
				.GetCustomAttributes(typeof(T), false)
				.Cast<T>()
				.SingleOrDefault();
			return attribute is null ?
				default(Expected)
				: expression(attribute);
		}
		public static string GetDescription(this Enum enumeration) => enumeration.GetAttributeValue<DescriptionAttribute, string>(x => x.Description);
		public static string ToDollars(this decimal d) => string.Format("$%,.2f", Math.Abs(d));
	}
}
