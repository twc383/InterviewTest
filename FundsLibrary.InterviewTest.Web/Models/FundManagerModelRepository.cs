using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Client;
using FundsLibrary.InterviewTest.Web.Models.Mappers;

namespace FundsLibrary.InterviewTest.Web.Models
{
	public interface IFundManagerModelRepository
	{
		Task<IEnumerable<FundManagerModel>> GetAll();
		Task<FundManagerModel> Get(Guid id);
	}

	public class FundManagerModelRepository : IFundManagerModelRepository
	{
		private readonly IServiceClient _client;
		private readonly IMapper<FundManager, FundManagerModel> _toModelMapper;
		private readonly IMapper<FundManagerModel, FundManager> _fromModelMapper;

		public FundManagerModelRepository(
			IServiceClient client = null, 
			IMapper<FundManager, FundManagerModel> toModelMapper = null, 
			IMapper<FundManagerModel, FundManager> fromModelMapper = null)
		{
			
			_client = client ?? new ServiceClient();
			_toModelMapper = toModelMapper ?? new ToFundManagerModelMapper();
			_fromModelMapper = fromModelMapper ?? new FromFundManagerModelMapper();
		}

		public async Task<IEnumerable<FundManagerModel>> GetAll()
		{
			var res = await	_client.GetAll();
			return res.Select(s => _toModelMapper.Map(s));
		}

		public async Task<FundManagerModel> Get(Guid id)
		{
			var res = await _client.GetById(id);
			return _toModelMapper.Map(res);
		}
	}
}