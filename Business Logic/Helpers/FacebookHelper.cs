using System.Net.Http;
using System.Threading.Tasks;
using Data_Transfer_Objects;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Business_Logic.Helpers
{
    internal class FacebookTokenModel
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
    }

    internal class FacebookDebugModel
    {
        internal class DataModel
        {
            [JsonProperty(PropertyName = "is_valid")]
            public bool IsValid { get; set; }
        }
        
        [JsonProperty(PropertyName = "data")]
        public DataModel Data { get; set; }
    }
    
    public class FacebookHelper
    {
        private readonly HttpClient httpClient;
        private string ClientId { get; }
        private string ClientSecret { get; }

        public FacebookHelper(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient();
            ClientId = configuration["Facebook:AppId"];
            ClientSecret = configuration["Facebook:AppSecret"];
        }

        public async Task<FacebookResponse> ReadFacebookAccountAsync(string userId, string token)
        {
            var request = $"https://graph.facebook.com/{userId}?fields=first_name,last_name,email,birthday&access_token={token}";
            
            var response = await httpClient.GetAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<FacebookResponse>(content);

            return result;
        }
        
        public async Task<bool> VerifyToken(string inputToken)
        {
            var token = await GenerateAccessToken();

            var request = $"https://graph.facebook.com/debug_token?input_token={inputToken}&access_token={token}";

            var response = await httpClient.GetAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<FacebookDebugModel>(content);

            return result.Data.IsValid;
        }
        
        private async Task<string> GenerateAccessToken()
        {
            var request = $"https://graph.facebook.com/oauth/access_token?client_id={ClientId}&client_secret={ClientSecret}&grant_type=client_credentials";

            var response = await httpClient.GetAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<FacebookTokenModel>(content);

            return result.AccessToken;
        }
    }
}