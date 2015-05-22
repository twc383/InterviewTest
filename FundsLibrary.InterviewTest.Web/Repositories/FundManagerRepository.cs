using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Models.Mappers;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IFundManagerRepository
    {
        Task<Boolean> Delete(Guid id);
        Task<IEnumerable<FundManager>> GetAll();
        Task<FundManager> Get(Guid id);
        Task<Guid> Put(FundManager content);
        Task<Guid> Post(FundManager content);
    }

    public class FundManagerRepository : IFundManagerRepository
    {
        private const string _serviceAppUrl = "http://localhost:50135/Service/";

        private readonly IHttpClientWrapper _client;
        private readonly IMapper<FundManagerDto, FundManager> _toModelMapper;

        public FundManagerRepository(
            IHttpClientWrapper client = null,
            IMapper<FundManagerDto, FundManager> toModelMapper = null)
        {
            _client = client ?? new HttpClientWrapper(_serviceAppUrl);
            _toModelMapper = toModelMapper ?? new MapFromFundManagerDtoToFundManager();
        }

        public async Task<IEnumerable<FundManager>> GetAll()
        {
            var managers = await _client.GetAndReadFromContentGetAsync<IEnumerable<FundManagerDto>>("api/FundManager/");
	        return managers.Select(s => _toModelMapper.Map(s));
        }

        public async Task<FundManager> Get(Guid id)
        {
            var manager = await _client.GetAndReadFromContentGetAsync<FundManagerDto>("api/FundManager/" + id);
            return _toModelMapper.Map(manager);
        }

        public async Task<Guid> Put(FundManager content)
        {
            var manager = await _client.PutContentAndGetAsync<Guid>("api/FundManager/", content);
            return manager;
        }

        public async Task<Guid> Post(FundManager content)
        {
            var manager = await _client.PostContentAndGetAsync<Guid>("api/FundManager/", content);
            return manager;
        }

        public async Task<Boolean> Delete(Guid id)
        {
            var manager = await _client.DeleteContentAndGetAsync<Boolean>(string.Format("api/FundManager/{0}", id));
            return manager;
        }
    }
}
