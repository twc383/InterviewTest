using System;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using System.Collections.Generic;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
    public interface IUserRepository
    {
        Task<Guid> Create(User user);
        Task<IQueryable<User>> GetAll();
        Task<User> GetBy(Guid id);
    }
}