using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReviewsService_Core.Domain.Form
{
    /// <summary>
    /// Review Form
    /// </summary>
    public class ReviewForm : BaseForm<Guid>
    {

        /// <summary>
        /// 
        /// </summary>   
        [JsonIgnore]
        public long AppClientId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Range(1, 5)]
        public int Rating { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string AppFeature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ReviewTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ParentId { get; set; }

    }
}
