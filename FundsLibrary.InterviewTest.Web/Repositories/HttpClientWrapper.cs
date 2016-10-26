using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IHttpClientWrapper
    {
        Task<T> GetAndReadFromContentGetAsync<T>(string requestUri);
        Task<TResponse> PutContentAndGetAsync<TResponse, TPut>(string requestUri, TPut content);
        Task<TResponse> PostContentAndGetAsync<TResponse, TPost>(string requestUri, TPost content);
        Task DeleteContentAsync(string requestUri);
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
                _SetupClient(client);

                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                //TODO: Handle non success HTTP codes more gracefully.

                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<TResponse> PutContentAndGetAsync<TResponse, TPut>(string requestUri, TPut content)
        {
            using (var client = new HttpClient())
            {
                _SetupClient(client);

                var response = await client.PutAsJsonAsync(requestUri, content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<TResponse>();
            }
        }

        public async Task<TResponse> PostContentAndGetAsync<TResponse, TPut>(string requestUri, TPut content)
        {
            using (var client = new HttpClient())
            {
                _SetupClient(client);

                var response = await client.PostAsJsonAsync(requestUri, content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<TResponse>();
            }
        }

        public async Task DeleteContentAsync(string requestUri)
        {
            using (var client = new HttpClient())
            {
                _SetupClient(client);

                var response = await client.DeleteAsync(requestUri);
                response.EnsureSuccessStatusCode();
            }
        }

        private void _SetupClient(HttpClient client)
        {
            client.BaseAddress = new Uri(_serviceAppUrl);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
