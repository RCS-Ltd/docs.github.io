using System;

namespace Streetwise.Api.Models
{
    public class LoginResponse
    {
        /// <summary>
        /// Your access token for futher authentification
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The datetime your token expires ( usually now + 10 mins ) 
        /// local server time
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// Error message, from ApiErrorMessages object
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Check if true login success, false means check the error message
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
