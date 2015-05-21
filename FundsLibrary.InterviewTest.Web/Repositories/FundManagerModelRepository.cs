using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Models.Mappers;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IFundManagerModelRepository
    {
        Task<Boolean> Delete(Guid id);
        Task<IEnumerable<FundManagerModel>> GetAll();
        Task<FundManagerModel> Get(Guid id);
        Task<Guid> Put(FundManagerModel content);
        Task<Guid> Post(FundManagerModel content);
    }

    public class FundManagerModelRepository : IFundManagerModelRepository
    {
        private const string _serviceAppUrl = "http://localhost:50135/Service/";

        private readonly IHttpClientWrapper _client;
        private readonly IMapper<FundManager, FundManagerModel> _toModelMapper;

        public FundManagerModelRepository(
            IHttpClientWrapper client = null,
            IMapper<FundManager, FundManagerModel> toModelMapper = null)
        {
            _client = client ?? new HttpClientWrapper(_serviceAppUrl);
            _toModelMapper = toModelMapper ?? new ToFundManagerModelMapper();
        }

        public async Task<IEnumerable<FundManagerModel>> GetAll()
        {
            var managers = await _client.GetAndReadFromContentGetAsync<IEnumerable<FundManager>>("api/FundManager/");
	        return managers.Select(s => _toModelMapper.Map(s));
        }

        public async Task<FundManagerModel> Get(Guid id)
        {
            var manager = await _client.GetAndReadFromContentGetAsync<FundManager>("api/FundManager/" + id);
            return _toModelMapper.Map(manager);
        }

        public async Task<Guid> Put(FundManagerModel content)
        {
            var manager = await _client.PutContentAndGetAsync<Guid>("api/FundManager/", content);
            return manager;
        }

        public async Task<Guid> Post(FundManagerModel content)
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
