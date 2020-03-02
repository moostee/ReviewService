using System;

namespace ReviewsService_Core.Common
{
    public static class CastUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int ToInt32(object p)
        {
            int resp = 0;
            try
            {
                resp = Convert.ToInt32(p);
            }
            catch { }
            return resp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static long ToInt64(object p)
        {
            long resp = 0;
            try
            {
                resp = Convert.ToInt64(p);
            }
            catch { }
            return resp;
        }
    }
}
