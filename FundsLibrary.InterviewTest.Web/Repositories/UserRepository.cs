using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using Microsoft.AspNet.Identity;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IUserRepository : IUserPasswordStore<User, Guid>, IUserLockoutStore<User, Guid>, IUserTwoFactorStore<User, Guid>
    {
        Task<IEnumerable<User>> GetAll();
    }

    public class UserRepository : IUserRepository
    {
        private readonly IHttpClientWrapper _client;

        public UserRepository(IHttpClientWrapper client)
        {
            _client = client;
        }

        public Task CreateAsync(User user)
        {
            return _client.PostContentAndGetAsync<Guid, User>("api/User/", user);
        }

        public Task UpdateAsync(User user)
        {
            return _client.PutContentAndGetAsync<Guid, User>("api/User/", user);
        }

        public Task DeleteAsync(User user)
        {
            return _client.DeleteContentAsync("api/User/" + user.Id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _client.GetAndReadFromContentGetAsync<IEnumerable<User>>("api/User/");
        }

        public async Task<User> FindByIdAsync(Guid id)
        {
            return await _client.GetAndReadFromContentGetAsync<User>("api/User/" + id);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _client.GetAndReadFromContentGetAsync<User>("api/User/" + userName);
        }

        public void Dispose()
        {}

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            return Task.FromResult(DateTimeOffset.Now);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }
    }
}
