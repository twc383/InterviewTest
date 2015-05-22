using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Web.Models;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IHttpClientWrapper
    {
        Task<T> GetAndReadFromContentGetAsync<T>(string requestUri);
        Task<T> PutContentAndGetAsync<T>(string requestUri, FundManager content);
        Task<T> PostContentAndGetAsync<T>(string requestUri, FundManager content);
        Task<T> DeleteContentAndGetAsync<T>(string requestUri);
    }

    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly string _serviceAppUrl;

        public HttpClientWrapper(string serviceAppUrl)
        {
            _serviceAppUrl = serviceAppUrl;
        }

        public async Task<T> GetAndReadFromContentGetAsync<T>(string requestUri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_serviceAppUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode(); 
                //TODO: Handle non success HTTP codes more gracefully.
                //Write to log
                //Exception will cause
                //Redirect to error page defined in web.config

                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<T> PutContentAndGetAsync<T>(string requestUri, FundManager content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_serviceAppUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var serialisedContent = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                StringContent contentString = new StringContent(serialisedContent, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync(requestUri, contentString);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<T> PostContentAndGetAsync<T>(string requestUri, FundManager content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_serviceAppUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var serialisedContent = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                StringContent contentString = new StringContent(serialisedContent, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(requestUri, contentString);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>(); 
            }
        }

        public async Task<T> DeleteContentAndGetAsync<T>(string requestUri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_serviceAppUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.DeleteAsync(requestUri);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>(); 
            }
        }
    }
}