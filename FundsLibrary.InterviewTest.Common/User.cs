using System;

namespace FundsLibrary.InterviewTest.Common
{
    public class User
    {
        public Guid Id { get; set; }
        public string username { get; set; }
        public string emailAddress { get; set; }
        public string password { get; set; }
        public DateTime registeredSince { get; set; }
        public role role { get; set; }
    }

    public enum role
    {
        Admin,
        ReadOnly
    }
    
}
