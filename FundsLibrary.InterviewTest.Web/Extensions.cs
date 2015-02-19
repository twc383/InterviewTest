using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FundsLibrary.InterviewTest.Web
{
	public static class Extensions
	{
		public static T Deserialize<T>(this JsonSerializer serializer, string json)
		{
			return serializer.Deserialize<T>(new JsonTextReader(new StringReader(json)));
		}

		private static IEnumerable<string> _GetQueryPairs(this NameValueCollection valuesByName)
		{
			return from string name in valuesByName
				   select String.Format("{0}={1}", HttpUtility.UrlEncode(name), HttpUtility.UrlEncode(valuesByName[name]));
		}

		public static string ToQueryString(this NameValueCollection valuesByName)
		{
			if (valuesByName == null || valuesByName.Keys.Count == 0)
				return string.Empty;

			var keyValuesStrings = _GetQueryPairs(valuesByName);

			return String.Join("&", keyValuesStrings);
		}
	}
}