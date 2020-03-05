using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Model
{
    public class AppClientModel : BaseModel<long>
    {
        public int AppId { get; set; }
        public int ClientId { get; set; }
        public string ClientSecret { get; set; }

    }
}
