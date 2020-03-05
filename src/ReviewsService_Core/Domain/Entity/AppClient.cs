using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Entity
{
    public class AppClient : BaseEntity<long>
    {
        public int AppId { get; set; }
        public int ClientId { get; set; }
        public string ClientSecret { get; set; }

    }
}
