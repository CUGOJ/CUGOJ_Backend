namespace CUGOJ.Backend.Tools.Params
{
    public static class Config
    {
        private static string _DBConnectionString = null!;
        public static string StoreDBConnectionString
        {
            get
            {
                if (_DBConnectionString == null)
                {
                    return null!;
                }
                return _DBConnectionString + "database=cugoj;";
            }
        }
        public static string OrleansDBConnectionString
        {
            get
            {
                if (_DBConnectionString == null)
                {
                    return null!;
                }
                return _DBConnectionString + "database=orleans;";
            }
        }
        public static bool UpdateDB { get; private set; } = false;
        public static int SiloPort { get; private set; } = 11111;
        public static int GatewayPort { get; private set; } = 30000;
        public static string Env { get; private set; } = null!;
        public static string IP { get; private set; } = null!;
        public static string ServiceName { get; private set; } = "Backend";
        public static string ServiceVersion { get; private set; } = "0.0.1";
        public static string ServiceID { get; private set; } = null!;
        public static bool Debug { get; private set; } = false;
        public static string TraceAddress { get; private set; } = null!;
        public static string LogAddress { get; private set; } = null!;
        public static void LoadProperties(string[] args)
        {
            var param = ParamParser.ParseArgs(args);
            if (param.ContainsKey("debug"))
            {
                Debug = true;
            }
            if (param.ContainsKey("db"))
            {
                _DBConnectionString = param["db"];
            }
            else if (!Debug)
                throw new Exception("缺失参数:数据库连接串");
            // if (param.ContainsKey("updateDB"))
            // {
            //     UpdateDB = true;
            // }
            // else
            //     throw new Exception("缺失参数:是否更新数据库");
            if (param.ContainsKey("siloport"))
            {
                SiloPort = int.Parse(param["siloport"]);
            }
            if (param.ContainsKey("gatewayport"))
            {
                GatewayPort = int.Parse(param["gatewayport"]);
            }
            if (param.ContainsKey("env"))
            {
                Env = param["env"];
            }
            else if (Debug)
                Env = "debug";
            else
                throw new Exception("缺失参数:环境信息");
        }
    }
}
