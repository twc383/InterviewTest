using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using Newtonsoft.Json;

namespace FundsLibrary.InterviewTest.Web.Client
{
	public class ServiceClient : IServiceClient
	{
		private readonly JsonSerializer _serializer;
		private readonly RestClient _fundManagerClient;

		private static readonly string ServiceAppUrl = "http://localhost:50135/Service/";

		public ServiceClient()
		{
			_serializer = new Newtonsoft.Json.JsonSerializer();
			_fundManagerClient = new RestClient(ServiceAppUrl + "api/FundManager/");
		}

		public async Task<IEnumerable<FundManager>> GetAll()
		{
			return _serializer.Deserialize<IEnumerable<FundManager>>(await _fundManagerClient.Get());
		}

		public async Task<FundManager> GetById(Guid id)
		{
			return _serializer.Deserialize<FundManager>(await _fundManagerClient.Get(id.ToString()));
		}
	}

	public interface IServiceClient
	{
		Task<IEnumerable<FundManager>> GetAll();
		Task<FundManager> GetById(Guid id);
	}
}