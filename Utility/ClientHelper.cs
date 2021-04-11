using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utility
{
    public class ClientHelper
    {
        public static async Task<HttpClient> GetHttpClientAsync(BaseServiceConfig config)
        {
            using (var client = new HttpClient())
            {

                Task<HttpResponseMessage> response =
                    client.PostAsync(config.BaseServiceEndPoint + config.TokenEndPoint, new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("username", config.BaseServiceUserName),
                        new KeyValuePair<string, string>("password", config.BaseServicePassword),
                        new KeyValuePair<string, string>("grant_type", "password")
                    }));

                if (!response.Result.IsSuccessStatusCode) return null;

                var resualt = await response.Result.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<TokenModel>(resualt);
                var resualtClient = new HttpClient { Timeout = Timeout.InfiniteTimeSpan };

                resualtClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {model.access_token}");
                return resualtClient;

            }

        }

        public static List<T> GetValue<T>(string relativeAddress, BaseServiceConfig config)
        {

            var httpClient = /*new HttpClient();*/GetHttpClientAsync(config).Result;

            if (httpClient == null)
                throw new UnauthorizedAccessException("امکان دسترسی به این منبع برای شما مهیا نمی باشد");

            HttpResponseMessage response = httpClient.GetAsync(config.BaseServiceEndPoint + relativeAddress).Result;

            if (!response.IsSuccessStatusCode) return null;

            var resualt = JsonConvert.DeserializeObject<List<T>>(response.Content.ReadAsStringAsync().Result);
            return resualt;
        }


        public static T GetScalarValue<T>(string relativeAddress, BaseServiceConfig config)
        {

            var httpClient = GetHttpClientAsync(config).Result;

            if (httpClient == null)
                throw new UnauthorizedAccessException("امکان دسترسی به این منبع برای شما مهیا نمی باشد");

            HttpResponseMessage response = httpClient.GetAsync(config.BaseServiceEndPoint + relativeAddress).Result;

            var resualt = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            return resualt;
        }

    }

}
