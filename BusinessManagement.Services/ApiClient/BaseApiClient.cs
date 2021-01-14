namespace BusinessManagement.Services.ApiClient
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class BaseApiClient : IBaseApiClient
    {
        internal string BaseUrl;
        public BaseApiClient(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public HttpResponseMessage GetResponse(string urlParm, string idToken = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                if (idToken != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + idToken);
                }
                var response = client.GetAsync(urlParm).Result;
                return response;
            }
        }

        public async Task<HttpResponseMessage> GetResponseAsync(string urlParm, string idToken = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                if (idToken != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + idToken);
                }
                HttpResponseMessage response = await client.GetAsync(urlParm);
                return response;
            }
        }

        public HttpResponseMessage DeleteResponse(string urlParm, string idToken = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                if (idToken != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + idToken);
                }
                HttpResponseMessage response = client.DeleteAsync(urlParm).Result;
                return response;
            }
        }

        public HttpResponseMessage PostResponse(string urlParm, string data, FormUrlEncodedContent parms = null, string idToken = null)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                if (idToken != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + idToken);
                }
                HttpContent content = new StringContent(data);

                if (parms != null & data == string.Empty)
                {
                    content = parms;
                }

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsync(urlParm, content).Result;
                return response;
            }
        }

        public async Task<HttpResponseMessage> PostResponseAsync(string urlParm, string data, string idToken = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                if (idToken != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + idToken);
                }
                HttpContent content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(urlParm, content);
                return response;
            }
        }

        public HttpResponseMessage PutResponse(string urlParm, string data, FormUrlEncodedContent parms = null, string idToken = null)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                if (idToken != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + idToken);
                }
                HttpContent content = new StringContent(data);

                if (parms != null & data == string.Empty)
                {
                    content = parms;
                }

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PutAsync(urlParm, content).Result;
                return response;
            }
        }

        public async Task<HttpResponseMessage> PutResponseAsync(string urlParm, string data, FormUrlEncodedContent parms = null, string idToken = null)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                if (idToken != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + idToken);
                }
                HttpContent content = new StringContent(data);

                if (parms != null & data == string.Empty)
                {
                    content = parms;
                }

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PutAsync(urlParm, content);
                return response;
            }
        }

    }
}
