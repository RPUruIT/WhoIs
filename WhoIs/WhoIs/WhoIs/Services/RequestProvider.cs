using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;
using WhoIs.Services.Interface;

namespace WhoIs.Services
{
    public class RequestProvider : IRequestProvider
    {
        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            try {
                HttpClient client = CreateClient();
                HttpResponseMessage response = await client.GetAsync(uri);
                HandleResponse(response);
                var getResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(getResult);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            return client;
        }

        private void HandleResponse(HttpResponseMessage response) {

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Error");
        }
    }
}
