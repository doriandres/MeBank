using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeBank.Services.API
{
    public class ApiClientManager : HttpClient, IApiClientManager
    {
        public const string BaseUrl = "https://www.gruposama.com/WebApiSecureSAMA/api";

        public async Task<T> GetFromApiAsync<T>(string endPoint)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, BaseUrl + endPoint);
            var response = await SendAsync(httpRequestMessage);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T> PostToApiAsync<T>(string endPoint, object data)
        {
            var dataToSend = JsonConvert.SerializeObject(data);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, BaseUrl + endPoint)
            {
                Content = new StringContent(dataToSend, Encoding.UTF8, "application/json")
            };
            var response = await SendAsync(httpRequestMessage);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}