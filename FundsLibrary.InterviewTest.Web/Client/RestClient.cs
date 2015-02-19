using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FundsLibrary.InterviewTest.Web.Client
{
	public class RestClient
	{
		private readonly string _address;

		public RestClient(string address)
		{
			_address = address;
		}

		private static HttpClient _GetClient()
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Add("user-agent", "Web");
			client.DefaultRequestHeaders.Add("Accept", "application/json");
			return client;
		}

		public async Task<string> Get(params string[] positionalParams)
		{
			using (var client = _GetClient())
			{
				var address = _address + String.Join("/", positionalParams);
				return await client.GetStringAsync(address);
			}
		}

		public async Task<string> Put(string data)
		{
			using (var client = _GetClient())
			{
				var result = await client.PutAsync(_address,new StringContent(data));
				return await result.Content.ReadAsStringAsync();
			}
		}

		public async Task<string> Post(string data)
		{
			using (var client = _GetClient())
			{
				var result = await client.PutAsync(_address, new StringContent(data));
				return await result.Content.ReadAsStringAsync();
			}
		}

		public async Task Delete()
		{
			using (var client = _GetClient())
			{
				await client.DeleteAsync(_address);
			}
		}
	}
}