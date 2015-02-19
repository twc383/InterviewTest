using System.Web.Mvc;

namespace FundsLibrary.InterviewTest.Web
{
	public static class FilterConfig
	{
		public static void ReigsterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}