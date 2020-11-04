using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetwise.Api.Models
{
    public class ApiResponse
    {
        /// <summary>
        /// True if all ok, false if errorMessage needs to be checked
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// An error message, if Success = false
        /// from ApiErrorMessages object
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// List errors from property validation
        /// </summary>
        public List<ValidationError> ValidationErrors { get; set; }

        /// <summary>
        /// Object of a single item.  be that of class, string int  etc
        /// used on getdata
        /// </summary>
        public object Item { get; set; }

        /// <summary>
        /// List of items to send. i.e Orders  or string messages
        /// used on getdata
        /// </summary>
        public List<object> Items { get; set; }
        
    }
}
