using ReviewsService_Core.Domain.Enum;
using System;

namespace ReviewsService_Core.Domain.Entity
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
