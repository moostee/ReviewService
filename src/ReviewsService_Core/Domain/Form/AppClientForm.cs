using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ReviewsService_Core.Domain.Form
{
    public class AppClientForm : BaseForm<long>
    {
        public int AppId { get; set; }
        public int ClientId { get; set; }

    }
}
