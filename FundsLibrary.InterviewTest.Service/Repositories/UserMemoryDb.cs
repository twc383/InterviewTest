using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FundsLibrary.InterviewTest.Common;
using System.Collections.Concurrent;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
    public class UserMemoryDb : IUserRepository
    {
        private static readonly ConcurrentDictionary<Guid, User> _users = new ConcurrentDictionary<Guid, User>();

        static UserMemoryDb()
        {
            //fake data
            var items = new[]
            {
                new User { Id = Guid.Parse("83eed26a-d1e3-4e78-8d3d-c4c9b63eb549"), emailAddress = "fm1@hl.co.uk", password = "password1", username = "fm1", registeredSince = DateTime.Now.AddYears(-3), role = role.Admin},
                new User { Id = Guid.Parse("11a12c9f-417f-4225-ba33-d05ce638b450"), emailAddress = "fm2@hl.co.uk", password = "password2", username = "fm2", registeredSince = DateTime.Now.AddYears(-2), role = role.ReadOnly},
                new User { Id = Guid.Parse("e5ada48d-bf76-4290-8e8a-c33b39e04d5f"), emailAddress = "fm3@hl.co.uk", password = "password3", username = "fm3", registeredSince = DateTime.Now.AddYears(-1), role = role.ReadOnly},
            };

            foreach (var user in items)
                _users.TryAdd(user.Id, user);
        }

        public Task<Guid> Create(User user)
        {
            user.Id = Guid.NewGuid();
            user.registeredSince = DateTime.Now;
            if (!_users.TryAdd(user.Id, user))
                throw new Exception("Cannot add user - another user with the same ID already exists."); // Unlikely as it's as the key is a GUID.
            return Task.FromResult(user.Id);
        }

        public Task<IQueryable<User>> GetAll()
        {
            return Task.FromResult(_users.Values.AsQueryable());
        }

        public Task<User> GetBy(Guid id)
        {
            User value;
            _users.TryGetValue(id, out value); // If the key is not in the dictionary return null.

            return Task.FromResult(value);
        }
    }
}