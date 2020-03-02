using Newtonsoft.Json;
using ReviewsService_Core.Domain.Enum;
using System;

namespace ReviewsService_Core.Domain.Model
{
    public class BaseModel<T>
    {

        [JsonProperty("id")]
        public T Id { get; set; }

        /// <summary>
        /// Record Status of item in storage
        /// </summary>
        /// 
        [JsonProperty("recordStatus")]
        public RecordStatus RecordStatus { get; set; }
        /// <summary>
        /// Date record was created
        /// </summary> 
        /// 
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Last time record was updated
        /// </summary> 
        /// 
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual object LogMe()
        {
            return Id;
        }
        /// <summary>
        /// Record Status Text
        /// </summary> 
        /// 
        [JsonProperty("recordStatusText")]
        public string RecordStatusText
        {
            get
            {
                return RecordStatus.ToString();
            }
        }

        /// <summary>
        /// Created Date text
        /// </summary> 
        /// 
        [JsonProperty("createdAtText")]
        public string CreatedAtText
        {
            get
            {
                return CreatedAt.ToString("dd-MMM-yy HH:mm");
            }
        }

        /// <summary>
        /// Updated Date Text
        /// </summary> 
        /// 
        [JsonProperty("updatedAtText")]
        public string UpdatedAtText
        {
            get
            {
                return UpdatedAt.ToString("dd-MMM-yy HH:mm");
            }
        }
    }
}
