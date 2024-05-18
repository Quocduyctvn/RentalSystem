namespace RentalSystem.WebConfig.Constant
{
    public class AuthConst
    {
        public const int NO_PERMISSION =    -1;


        public static class AppCategory
        {
            public const int VIEW_LIST =    1101;
            public const int CREATE =       1102;
            public const int UPDATE =       1103;
            public const int DELETE =       1104;
            public const int VIEW_DETAIL =  1105;
        }
        public static class AppContacts
        {
            public const int VIEW_LIST =    1201;
            public const int VIEW_DETAIL =  1202;
            public const int FEEDBACK =     1203;
            public const int DELETE =       1204;
        }
        public static class AppHistoryPayments
        {
            public const int VIEW_LIST =    1301;
            public const int VIEW_DETAIL =  1302;
        }

        public static class AppPosts
        {
            public const int VIEW_LIST =    1501;
            public const int CREATE =       1502;
            public const int UPDATE =       1503;
            public const int DELETE =       1504;
            public const int PUBLIC =       1505;
            public const int VIEW_DETAIL =  1506;
			public const int REVIEW = 1507;  // duyệt bài 

		}
		public static class AppRoles
        {
            public const int VIEW_LIST =    1601;
            public const int CREATE =       1602;
            public const int UPDATE =       1603;
            public const int DELETE =       1604;
        }
        public static class AppTypeOfService
        {
            public const int VIEW_LIST =    1701;
            public const int CREATE =       1702;
            public const int UPDATE =       1703;
            public const int DELETE =       1704;
        }
        public static class AppUsers
        {
            public const int VIEW_LIST =    1801;
            public const int CREATE =       1802;
            public const int UPDATE =       1803;
            public const int UPDATE_PWD =   1804;
            public const int IsCHANGEBLOCK =        1805;
            public const int DELETE =       1806;
        }
		public static class AppPolicy
		{
			public const int VIEW_LIST = 1901;
			public const int CREATE = 1902;
			public const int UPDATE = 1903;
			public const int DELETE = 1904;
			public const int DETAIL = 1905;
		}
	}
}
