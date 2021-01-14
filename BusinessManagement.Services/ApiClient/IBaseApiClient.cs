namespace BusinessManagement.Services.ApiClient
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IBaseApiClient
    {
        HttpResponseMessage GetResponse(string urlParm, string idToken = null);
        HttpResponseMessage DeleteResponse(string urlParm, string idToken = null);
        Task<HttpResponseMessage> GetResponseAsync(string urlParm, string idToken = null);
        HttpResponseMessage PostResponse(string urlParm, string data, FormUrlEncodedContent parms = null, string idToken = null);
        Task<HttpResponseMessage> PostResponseAsync(string urlParm, string data, string idToken = null);
    }
}