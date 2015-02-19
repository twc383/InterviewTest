using System.IO;
using Newtonsoft.Json;

namespace FundsLibrary.InterviewTest.Web
{
	public static class Extensions
	{
		public static T Deserialize<T>(this JsonSerializer serializer, string json)
		{
			return serializer.Deserialize<T>(new JsonTextReader(new StringReader(json)));
		}
	}
}