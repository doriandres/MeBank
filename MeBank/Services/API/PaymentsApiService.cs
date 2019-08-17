using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using Newtonsoft.Json;

namespace MeBank.Services.API
{
    public class PaymentsApiService
    {
        private HttpClient httpClient;

        public PaymentsApiService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Payment>> GetPaymentsAsync(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("https://www.gruposama.com/WebApiSecureSAMA/api/Pago");
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Payment>>(responseData);
        }

        public async Task<Payment> AddPaymentAsync(Payment payment, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonDataToSend = JsonConvert.SerializeObject(payment);
            var response = await httpClient.PostAsync("https://www.gruposama.com/WebApiSecureSAMA/api/pago/ingresar", new StringContent(jsonDataToSend, Encoding.UTF8, "application/json"));
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Payment>(responseData);
        }
    }
}