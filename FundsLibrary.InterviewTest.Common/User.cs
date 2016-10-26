using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace FundsLibrary.InterviewTest.Common
{
    public class User : IUser<Guid>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegisteredSince { get; set; }
        public IReadOnlyCollection<Role> Roles { get; set; }
    }
}
