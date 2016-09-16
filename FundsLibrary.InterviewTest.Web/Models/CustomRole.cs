using FundsLibrary.InterviewTest.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FundsLibrary.InterviewTest.Web.Models
{
    public class CustomRole
    {
        private readonly IUserModelRepository _repository;

        public CustomRole()
            : this(new UserModelRepository())
        { }

        public CustomRole(IUserModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsUserInRole(Guid id, string role)
        {
            var user = await _repository.Get(id);
            if (id != null)
            {
                if (user.Role.ToString() == role)
                {
                    return true;
                }
            }
            return false;
        }
    }
}