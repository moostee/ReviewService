using System;

namespace ReviewsService_Core.Common
{
    class DateUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime CurrentDateTime()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <param name="fiscaloffset"></param>
        /// <returns></returns>
        public static int MonthQuarter(int month, int fiscaloffset = 0)
        {
            if (month < 1) month = 1;
            if (month > 12) month = 12;
            var k = (month / 12.0) * 4.0;
            var l = (int)Math.Ceiling(k);
            return l + fiscaloffset;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <param name="fiscaloffset"></param>
        /// <returns></returns>
        public static int MonthSemester(int month, int fiscaloffset = 0)
        {
            if (month < 1) month = 1;
            if (month > 12) month = 12;
            var k = (month / 12.0) * 2.0;
            var l = (int)Math.Ceiling(k);
            return l + fiscaloffset;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static int MinuteHalf(int minute)
        {
            if (minute < 1) minute = 60;
            if (minute > 59) minute = 60;
            var k = (minute / 60.0) * 2.0;
            var l = (int)Math.Ceiling(k);
            return l;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static int MinuteThird(int minute)
        {
            if (minute < 1) minute = 60;
            if (minute > 59) minute = 60;
            var k = (minute / 60.0) * 3.0;
            var l = (int)Math.Ceiling(k);
            return l;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static int MinuteSixth(int minute)
        {
            if (minute < 1) minute = 60;
            if (minute > 59) minute = 60;
            var k = (minute / 60.0) * 6.0;
            var l = (int)Math.Ceiling(k);
            return l;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static int MinuteQuarter(int minute)
        {
            if (minute < 1) minute = 60;
            if (minute > 59) minute = 60;
            var k = (minute / 60.0) * 4.0;
            var l = (int)Math.Ceiling(k);
            return l;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtm"></param>
        /// <returns></returns>
        public static int ToDateKey(DateTime dtm)
        {
            return Convert.ToInt32(dtm.ToString("yyyyMMdd"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtm"></param>
        /// <returns></returns>
        public static int ToWeekKey(DateTime dtm, int week)
        {
            return Convert.ToInt32(dtm.ToString("yyyy") + week.ToString("00"));
        }
    }
}
