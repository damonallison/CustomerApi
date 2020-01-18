using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Tests
{
    public class AuthenticationTest
    {

        private async Task<string> GetAccessToken() {

            const string TokenUrl = "https://dev-201383.okta.com/oauth2/default/v1/token";

            //
            // TODO: Pull from config file
            //
            var clientId = "0oayz1gm0PDLREvIh4x5";
            var secret = "6V7tem1Ib_h4u18XmttJFzCpCfA8QnxxCAB5is79";
            var clientCreds = System.Text.Encoding.UTF8.GetBytes($"{clientId}:{secret}");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(clientCreds));

            var postMessage = new Dictionary<string, string>()
            {
                ["grant_type"] = "client_credentials",
                ["scope"] = "access_token"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, TokenUrl)
            {
                Content = new FormUrlEncodedContent(postMessage)
            };

            var response = await client.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public void TestGetAccessToken()
        {
            Console.WriteLine(GetAccessToken().Result);
        }
    }
}
