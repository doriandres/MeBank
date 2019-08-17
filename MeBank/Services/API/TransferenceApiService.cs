using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using Newtonsoft.Json;

namespace MeBank.Services.API
{
    public class TransferenceApiService
    {
        private HttpClient httpClient;

        public TransferenceApiService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Transfer>> GetTransferencesAsync(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("https://www.gruposama.com/WebApiSecureSAMA/api/Transferencia");
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Transfer>>(responseData);
        }

        public async Task<Transfer> AddTransferenceAsync(Transfer transference, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonDataToSend = JsonConvert.SerializeObject(transference);
            var response = await httpClient.PostAsync("https://www.gruposama.com/WebApiSecureSAMA/api/transferencia/ingresar", new StringContent(jsonDataToSend, Encoding.UTF8, "application/json"));
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Transfer>(responseData);
        }
    }
}