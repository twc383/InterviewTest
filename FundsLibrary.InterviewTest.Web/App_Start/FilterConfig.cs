using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
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
