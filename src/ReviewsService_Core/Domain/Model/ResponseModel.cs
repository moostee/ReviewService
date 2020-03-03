using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Model
{
    public class ResponseModel
    {
        public string RequestId { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object Data { get; set; }
    }
}
