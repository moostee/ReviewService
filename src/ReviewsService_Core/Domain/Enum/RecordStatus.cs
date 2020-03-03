namespace ReviewsService_Core.Domain.Enum
{
    public enum RecordStatus
    {
        /// <summary>
        /// Pending state
        /// </summary>
        Pending,
        /// <summary>
        /// Flagged as active
        /// </summary>
        Active,
        /// <summary>
        /// Flagged as inactive
        /// </summary>
        Inactive,
        /// <summary>
        /// Soft deleted
        /// </summary>
        Deleted,
        /// <summary>
        /// Archivere can delete and archive record
        /// </summary>
        Archive
    }

}
