using E_Test.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;
using System.Text;

namespace E_Test.HelperAPI
{
    public class AccountAPI
    {
        private readonly HttpClient _httpClient;
        public AccountAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44308/api/Account/");
        }
        public async Task<List<RolesVM>> GetRoles(string endpointName)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + endpointName);
            var response = await _httpClient.SendAsync(httpRequest);
            if (response.IsSuccessStatusCode)
            {
                var responceContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<RolesVM>>(responceContent);
                return result;
            }
            else
            {
                return null;
            }
        }
        public async Task<string> LoginAuth(LoginVM model, string endpointName)
        {
            var json = JsonConvert.SerializeObject(model);
            // cast string json to HttpContent
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_httpClient.BaseAddress + endpointName, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            return null;

        }
    }
}
