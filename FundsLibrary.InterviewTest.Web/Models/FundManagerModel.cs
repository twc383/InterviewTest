using System;
using System.ComponentModel.DataAnnotations;
using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Web.Models
{
	public class FundManagerModel
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Biography { get; set; }
		public Location Location { get; set; }
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime ManagedSince { get; set; }
	}
}
