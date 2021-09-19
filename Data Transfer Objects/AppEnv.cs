using System;

namespace Data_Transfer_Objects
{
    public class AppEnv
    {
        public class Roles
        {
            public const string Master = "Master";
            public const string Admin = "Admin";
            public const string Customer = "Customer";
        }

        public class Dates
        {
            public static TimeSpan In30Days = TimeSpan.FromDays(30);
            public static TimeSpan In7Days = TimeSpan.FromDays(30);
            public static TimeSpan In1Day = TimeSpan.FromDays(1);
        }
    }
}
