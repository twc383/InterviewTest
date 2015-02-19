using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Service.Repositories;

namespace FundsLibrary.InterviewTest.Service.Controllers
{
	public class FundManagerController : ApiController
    {
	    private readonly IFundManagerRepository _repository;

		public FundManagerController()
			:this(null)
		{
			
		}

	    public FundManagerController(IFundManagerRepository injectedRepository = null)
		{
			_repository = injectedRepository ?? new FundManagerMemoryDb();
		}


		public async Task<IEnumerable<FundManager>> Get()
		{
			return (await _repository.GetBy()).ToArray();
		}

		// GET: api/FundManager/79c74c79-f993-454e-a7d4-53791f17f179
        public async Task<FundManager> Get(Guid id)
        {
			return await _repository.GetBy(id);
        }
    }
}
