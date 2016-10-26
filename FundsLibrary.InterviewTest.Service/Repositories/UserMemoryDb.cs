using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
    public class UserMemoryDb : IUserRepository
    {
        private static readonly ConcurrentDictionary<Guid, User> _users = new ConcurrentDictionary<Guid, User>();

        public Task<Guid> Create(User user)
        {
            user.Id = Guid.NewGuid();
            user.RegisteredSince = DateTime.Now;
            if (!_users.TryAdd(user.Id, user))
                throw new Exception("Cannot add user - another user with the same ID already exists."); // Unlikely as the key is a GUID.
            return Task.FromResult(user.Id);
        }

        public Task<IQueryable<User>> GetAll()
        {
            return Task.FromResult(_users.Values.AsQueryable());
        }

        public Task<User> GetById(Guid id)
        {
            User value;
            _users.TryGetValue(id, out value); // If the key is not in the dictionary return null.

            return Task.FromResult(value);
        }

        public Task<User> GetByUsername(string username)
        {
            var user = _users.Values.SingleOrDefault(u => u.UserName == username);
            return Task.FromResult(user);
        }
    }
}
