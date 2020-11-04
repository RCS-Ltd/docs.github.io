using Newtonsoft.Json;
using Streetwise.Api.Connect.Helpers;
using Streetwise.Api.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Streetwise.Api.Connect
{
    public class ApiAccessService
    {
        protected readonly HttpClient _client = new HttpClient();

        public ApiAccessService()
        {
            SetHeaders();
        }

        /// <summary>
        /// Logs into the stores api and gets an access token
        /// </summary>
        /// <param name="clientId">client id string</param>
        /// <param name="clientSecret">client secret string</param>
        /// <param name="storeUrl">must end with a forward slash, usually stored in your app settings.</param>
        /// <returns>Object type of LoginResponse</returns>
        public async Task<LoginResponse> GetLogin(string clientId, string clientSecret, string apiUrl)
        {
            try
            {
                var loginResponse = await _client.PostAsync($"{apiUrl}{ApiAuthEndpoints.Login}", CommonHelper.JsonContent(new LoginModel
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                }));

                if (loginResponse.IsSuccessStatusCode)
                {
                    var loginStatus = JsonConvert.DeserializeObject<LoginResponse>(await loginResponse.Content.ReadAsStringAsync());

                    return loginStatus;
                }

                return new LoginResponse
                {
                    IsSuccess = false,
                    ErrorMessage = $"{ApiErrorMessages.TransportError} {loginResponse.StatusCode} and message {loginResponse.ReasonPhrase}"
                };
            }
            catch (System.Exception e)
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
            }
            
        }

        /// <summary>
        /// Allows the user to extend the life of an existing, valid token
        /// </summary>
        /// <param name="existingToken">Your valid token</param>
        /// <param name="storeUrl">must end with a forward slash, usually stored in your app settings.</param>
        /// <returns>object type of LoginResponse</returns>
        public async Task<LoginResponse> RefreshLogin(string existingToken, string apiUrl)
        {
            try
            {
                var request = await _client.PostAsync($"{apiUrl}{ApiAuthEndpoints.RenewToken}", CommonHelper.JsonContent(
                new RefreshToken { AccessToken = existingToken }));

                if (request.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<LoginResponse>(await request.Content.ReadAsStringAsync());
                    return response;
                }

                return new LoginResponse
                {
                    IsSuccess = false,
                    ErrorMessage = $"{ApiErrorMessages.TransportError} {request.StatusCode} and message {request.ReasonPhrase}"
                };
            }
            catch (System.Exception e)
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
            }
        }

        /// <summary>
        /// Get items from the api
        /// </summary>
        /// <param name="requestModel">request model</param>
        /// <param name="storeUrl">Store URL</param>
        /// <param name="endpoint">Api Endpoint</param>
        /// <returns>ApiResponse object</returns>
        public async Task<ApiResponse> GetData(RequestModel requestModel, string apiUrl, string endpoint)
        {
            var validater = Validate(requestModel);

            if (!validater.Success)
                return validater;

            var request = await _client.PostAsync($"{apiUrl}{endpoint}", CommonHelper.JsonContent(requestModel));

            if(request.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiResponse>(await request.Content.ReadAsStringAsync());


            return Failed(request);
        }

        /// <summary>
        /// Send an item for insert or update
        /// </summary>
        /// <param name="item">object must have a base of BAseModel</param>
        /// <param name="storeUrl">URL for the store, usually in app.config</param>
        /// <param name="endpoint">the APIEndpoint</param>
        /// <returns>ApiResponse object</returns>
        public async Task<ApiResponse> SendData(RequestModel requestModel, string apiUrl, string endpoint)
        {
            return await GetData(requestModel, apiUrl, endpoint);
        }                

        private void SetHeaders()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("User-Agent", "streetwise.Api.Connect");
        }

        private ApiResponse Failed(string message)
        {
            return new ApiResponse { Success = false, ErrorMessage = $"{message}" };
        }

        private ApiResponse Failed(HttpResponseMessage response)
        {
            return new ApiResponse { Success = false, ErrorMessage = $"{ApiErrorMessages.TransportError} {response.StatusCode} and message {response.ReasonPhrase}" };
        }

        private ApiResponse Validate(RequestModel model)
        {
            if (string.IsNullOrEmpty(model.AccessToken))
                return Failed(ApiErrorMessages.MissingAccessToken);

            if (string.IsNullOrEmpty(model.Data))
                return Failed(ApiErrorMessages.NoValidContent);

            return new ApiResponse { Success = true };
        }
    }
}
