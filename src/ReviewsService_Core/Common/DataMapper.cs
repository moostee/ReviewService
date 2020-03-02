namespace ReviewsService_Core.Common
{
    public class DataMapper
    {
        /// <summary>
        /// Map destination type from source type 
        /// </summary>
        /// <typeparam name="T1">Destination type</typeparam>
        /// <typeparam name="T2">Sourcetype</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T1 Map<T1, T2>(T2 obj)
        {
            var mapper = AutoMapperConfig.config.CreateMapper();
            return mapper.Map<T1>(obj);
        }
    }
}
