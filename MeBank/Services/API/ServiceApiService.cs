using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using Newtonsoft.Json;

namespace MeBank.Services.API
{
    public class ServiceApiService
    {
        private HttpClient httpClient;

        public ServiceApiService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Service>> GetServicesAsync()
        {
            var response = await httpClient.GetAsync("https://www.gruposama.com/WebApiSecureSAMA/api/Servicio");
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Service>>(responseData);
        }

        public async Task<Service> AddServiceAsync(Service service)
        {
            var jsonDataToSend = JsonConvert.SerializeObject(service);
            var response = await httpClient.PostAsync("https://www.gruposama.com/WebApiSecureSAMA/api/servicio/ingresar", new StringContent(jsonDataToSend));
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Service>(responseData);
        }

        public async Task<Service> ModifyServiceAsync(Service service)
        {
            var jsonDataToSend = JsonConvert.SerializeObject(service);
            var response = await httpClient.PutAsync("https://www.gruposama.com/WebApiSecureSAMA/api/Servicio", new StringContent(jsonDataToSend));
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Service>(responseData);
        }

        public async Task<Service> RemoveServiceAsync(int id)
        {
            var response = await httpClient.DeleteAsync("https://www.gruposama.com/WebApiSecureSAMA/api/Servicio/" + id);
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Service>(responseData);
        }
    }
}