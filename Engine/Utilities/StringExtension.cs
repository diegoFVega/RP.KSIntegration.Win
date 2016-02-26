using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Engine.Utilities
{
	public static class StringExtension
	{
		public static string FixJsonUrl(this string url, SortedList<string, string> parameters = null)
		{
			var strParameters = new StringBuilder();
			var num = 1;
			if (parameters != null)
			{
				foreach (var pair in parameters)
				{
					strParameters.AppendFormat((num < parameters.Count ? "{0}={1}&" : "{0}={1}"), pair.Key, pair.Value);
					num++;
				}
			}
			return ((parameters != null) ? string.Format("{0}?{1}", url, strParameters.ToString()) : url);
		}

		public static string FixJsonUrl(this string url, string parameters = null) => ((parameters != null) ? string.Format("{0}?{1}", url, parameters) : url);

		public static MailAddress ToMailAddress(this string address) => new MailAddress(address);

		public static MailAddressCollection ToMailAddressCollection(this string addresses, char[] splitCharacter)
		{
			var addresss = new MailAddressCollection();
			foreach (var str in addresses.Split(splitCharacter))
			{
				addresss.Add(str);
			}
			return addresss;
		}

		public static bool IsAValidString(this string textString, string expression)
		{
			var regEx = new Regex(expression);
			return (regEx.IsMatch(textString));
		}
	}
}