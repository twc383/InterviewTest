using System;
using System.ComponentModel.DataAnnotations;
using FundsLibrary.InterviewTest.Common;
using System.ComponentModel;

namespace FundsLibrary.InterviewTest.Web.Models
{
    public class FundManagerModel
    {
        [DisplayName("Id")]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Biography")]
        public string Biography { get; set; }

        [DisplayName("Location")]
        public Location Location { get; set; }

        [DisplayName("Managed since")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ManagedSince { get; set; }
    }
}
