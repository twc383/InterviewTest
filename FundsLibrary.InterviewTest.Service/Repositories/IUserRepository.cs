using System;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
    public interface IUserRepository
    {
        Task<Guid> Create(User user);
        Task<IQueryable<User>> GetAll();
        Task<User> GetById(Guid id);
        Task<User> GetByUsername(string username);
    }
}
