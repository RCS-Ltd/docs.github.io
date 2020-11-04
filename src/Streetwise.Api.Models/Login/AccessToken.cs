using System;
using System.Collections.Generic;

namespace Streetwise.Api.Models
{
    public class AccessToken
    {
        public DateTime IssueTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public string ClientId { get; set; }
        public string UserGuid { get; set; }
        public string ClientIp { get; set; }
        public List<string> Claims { get; set; }
        public string StoreId { get; set; }
    }
}
