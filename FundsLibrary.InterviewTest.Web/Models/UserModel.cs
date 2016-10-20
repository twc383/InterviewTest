using FundsLibrary.InterviewTest.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FundsLibrary.InterviewTest.Web.Models
{
    public class UserModel
    {
        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        public Guid Id { get; set; }

        [DisplayName("Registered Since")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RegisteredSince { get; set; }

        [DisplayName("Role")]
        public role Role { get; set; }

        public bool isLoggedIn { get; set; }
    }
}