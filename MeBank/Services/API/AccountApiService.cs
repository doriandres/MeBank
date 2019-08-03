using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using Newtonsoft.Json;

namespace MeBank.Services.API
{
    public class AccountApiService
    {
        private HttpClient httpClient;

        public AccountApiService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Account>> GetAccountsAsync(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("https://www.gruposama.com/WebApiSecureSAMA/api/Cuenta");
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Account>>(responseData);
        }

        public async Task<Account> AddAccountAsync(Account accounts, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonDataToSend = JsonConvert.SerializeObject(accounts);
            var response = await httpClient.PostAsync("https://www.gruposama.com/WebApiSecureSAMA/api/cuenta/ingresar", new StringContent(jsonDataToSend));
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Account>(responseData);
        }

        public async Task<Account> ModifyAccountAsync(Account account, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonDataToSend = JsonConvert.SerializeObject(account);
            var response = await httpClient.PutAsync("https://www.gruposama.com/WebApiSecureSAMA/api/Cuenta", new StringContent(jsonDataToSend));
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Account>(responseData);
        }

        public async Task<Account> RemoveAccountAsync(int id, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.DeleteAsync("https://www.gruposama.com/WebApiSecureSAMA/api/Cuenta/" + id);
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Account>(responseData);
        }
    }
}