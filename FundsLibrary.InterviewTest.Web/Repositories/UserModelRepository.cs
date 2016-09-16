using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Models.Mappers;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IUserModelRepository
    {
        Task<Guid> Post(UserModel User);
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> Get(Guid id);
    }

    public class UserModelRepository : IUserModelRepository
    {
        private const string _serviceAppUrl = "http://localhost:50135/Service/";

        private readonly IHttpClientWrapper _client;
        private readonly IMapper<User, UserModel> _toModelMapper;

        public UserModelRepository(
            IHttpClientWrapper client = null,
            IMapper<User, UserModel> toModelMapper = null)
        {
            _client = client ?? new HttpClientWrapper(_serviceAppUrl);
            _toModelMapper = toModelMapper ?? new ToUserModelMapper();
        }

        public async Task<Guid> Post(UserModel content)
        {
            var user = await _client.PostContentAndGetAsync<Guid>("api/User/", content);
            return user;
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var users = await _client.GetAndReadFromContentGetAsync<IEnumerable<User>>("api/User/");
            return users.Select(s => _toModelMapper.Map(s)).OrderBy(s => s.RegisteredSince);
        }

        public async Task<UserModel> Get(Guid id)
        {
            var user = await _client.GetAndReadFromContentGetAsync<User>("api/User/"+id);
            return _toModelMapper.Map(user);
        }
    }
}