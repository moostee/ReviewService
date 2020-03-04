using System.Text.Json.Serialization;

namespace ReviewsService_Core.Domain.Form
{
    public class BaseForm<T>
    {
        [JsonIgnore]
        public T Id { get; set; }
    }
}
