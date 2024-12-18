namespace WebMMP.Scripts
{
    public static class TimeOperations
    {
        public static string ConvertTime(DateTime time)
        {
            if (time < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 40, 0))
                return "09:00:00";
            if (time < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 50, 0))
                return "10:40:00";
            if (time < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 30, 0))
                return "12:50:00";
            if (time < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 10, 0))
                return "14:30:00";
            if (time < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 50, 0))
                return "16:10:00";
            if (time < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 30, 0))
                return "17:50:00";
            return "19:30:00";
        }

        public static int GetPairNumber(DateTime time)
        {
            if (time.TimeOfDay < new TimeSpan(0, 10, 30, 0))
                return 1;
            if (time.TimeOfDay < new TimeSpan(0, 12, 10, 0))
                return 2;
            if (time.TimeOfDay < new TimeSpan(0, 14, 20, 0))
                return 3;
            if (time.TimeOfDay < new TimeSpan(0, 16, 00, 0))
                return 4;
            if (time.TimeOfDay < new TimeSpan(0, 17, 40, 0))
                return 5;
            if (time.TimeOfDay < new TimeSpan(0, 19, 20, 0))
                return 6;
            return 7;
        }
    }
}
