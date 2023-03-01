using CUGOJ.Tools.Infra.ConfigProvider;
using CUGOJ.Share;
using CUGOJ.Tools.Common;
using CUGOJ.Tools.Infra.HttpProvider;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PostSharp.Reflection;
using System.Net;
using System.Reflection;

namespace CUGOJ.Tools
{
    public static partial class Config
    {
        private static HashSet<string> existKeys = new();
        private static HashSet<string> visited = new();
        private static Dictionary<string, PropertyInfo> properties = new();
        private static Dictionary<string, ConfigItemAttribute> attributes = new();
        private static List<string> requiredKey = new();
        private static List<string> requiredInjectKey = new();
        public static void InitConfig(string[] args, bool AllowReplicate = false)
        {
            string? key;
            // 获取需要被加载的配置
            foreach (var property in typeof(Config).GetProperties())
            {
                var props = property.GetCustomAttributes<ConfigItemAttribute>(false);
                var attr = property.GetCustomAttributes<ConfigItemAttribute>(false).FirstOrDefault();
                if (attr != null)
                {
                    attr.PropertyName= property.Name;
                    attr.TargetType = property.PropertyType;
                    key = attr.Key;
                    // 判断重名配置
                    if (existKeys.Contains(key))
                        throw new Exception($"{key} 这一配置在Config类中出现了两次,由于属性不允许重名,所以请检查是否有ParsableAttribute的Key设置重复或与属性名重复");
                    existKeys.Add(key);
                    // 加载必填项
                    if (attr.RequiredInject)
                    {
                        requiredKey.Add(key);
                        requiredInjectKey.Add(key);
                    }
                    else if (attr.Required)
                        requiredKey.Add(key);
                    properties[key] = property;
                    attributes[key] = attr;
                }
            }
            // 清空用作启动参数的去重
            existKeys.Clear();
            key = null;
            // 尝试读取一个参数值
            var ReadValueHandler = (string arg) =>
            {
                if (key == null) return false;
                if (!properties.TryGetValue(key, out var property) || !attributes.TryGetValue(key, out var attr))
                    throw new Exception($"注入了未知的参数:{key}");
                if (!attr.CanInject)
                    throw new Exception($"配置{key}不允许参数注入");
                if (arg.StartsWith("-"))
                {
                    if (property.PropertyType == typeof(bool))
                    {
                        property.SetValue(null, true);
                        visited.Add(key);
                        key = null;
                        return false;
                    }
                    else
                        throw new Exception($"配置项{key}的类型为{property.PropertyType},但是没有设置其值,请检查注入参数的-{key}后面的内容");
                }
                try
                {
                    property.SetValue(null, CommonTools.ParseString(property.PropertyType, arg));
                    visited.Add(key);
                    key = null;
                }
                catch (Exception ex)
                {
                    throw new Exception($"配置项{key}的值错误：` {arg} ` 无法被解析为{property.PropertyType}类型\n InnerException={ex.Message}");
                }
                return true;
            };
            // 尝试读取一个参数键
            var ReadKeyHandler = (string arg) =>
            {
                if (!arg.StartsWith("-"))
                    throw new Exception($"未能成功解析参数,期望以外的参数:{arg}");
                key = arg[1..];
                if (key == "")
                    throw new Exception($"错误的参数: - ,请在-后加入配置的Key来注入配置");
                if (!properties.ContainsKey(key))
                    throw new Exception($"错误的参数: {arg}, 参数键{key}没有匹配到任何一个接受注入的配置");
                if (!AllowReplicate && existKeys.Contains(key))
                    throw new Exception($"重复设置参数:{key}");
                existKeys.Add(key);
            };
            // 解析参数
            foreach (var arg in args)
            {
                if (!ReadValueHandler(arg))
                    ReadKeyHandler(arg);
            }
            // 空解析,适配最后一个参数键是Bool的情况
            ReadValueHandler("-");
            // 判断必须参数注入项
            foreach (var item in requiredInjectKey)
            {
                if (!visited.Contains(item))
                    throw new Exception(attributes[item].NotInjectMsg);
            }
        }

        public static async Task InitRemoteConfig()
        {
            string? key;
            var configProvider = new ConfigProvider(new HttpProvider());
            ConfigItemAttribute.ConfigProvider = configProvider;
            if (configProvider != null)
            {
                // 加载远程配置
                if (!string.IsNullOrEmpty(AdminAddress))
                {
                    await configProvider.Init();
                    foreach (var item in properties)
                    {
                        var property = item.Value;
                        var attr = attributes[item.Key];
                        if (attr.CanRemote)
                        {
                            key = attr.Key;
                            if (!visited.Contains(key))
                            {
                                if (await configProvider.CheckExist(key, property.PropertyType))
                                {
                                    property.GetValue(null);
                                    visited.Add(key);
                                }
                            }
                        }
                    }
                }
            }

            // 判断必填项
            foreach (var item in requiredKey)
            {
                if (!visited.Contains(item))
                    throw new Exception(attributes[item].NotSetMsg);
            }
        }


        private static string dbConnectionSrting = null!;

        [ConfigItem(Required = true,
            ConfigType = ConfigItemAttribute.ConfigTypeEnum.RemoteIfNotEdit | ConfigItemAttribute.ConfigTypeEnum.Inject)]
        public static string DBConnectionString
        {
            get => dbConnectionSrting + (AllowUnsafeSSL ? "Encrypt=False;" : string.Empty);
            set => dbConnectionSrting = value;
        }
        public static string StoreDBConnectionString
        {
            get
            {
                if (DBConnectionString == null)
                {
                    return null!;
                }
                return DBConnectionString + "database=cugoj;";
            }
        }
        public static string OrleansDBConnectionString
        {
            get
            {
                if (DBConnectionString == null)
                {
                    return null!;
                }
                return DBConnectionString + "database=orleans;";
            }
        }
        public static bool UpdateDB { get; private set; } = false;
        [ConfigItem(ConfigType = ConfigItemAttribute.ConfigTypeEnum.Inject)]
        public static int SiloPort { get; private set; } = 11111;

        [ConfigItem(ConfigType = ConfigItemAttribute.ConfigTypeEnum.Inject)]
        public static int GatewayPort { get; private set; } = 30000;
        [ConfigItem(ConfigType = ConfigItemAttribute.ConfigTypeEnum.Inject, RequiredInject = true)]
        public static string Env { get; private set; } = null!;
        public static string IP { get; private set; } = null!;
        public static string ServiceName { get; private set; } = "Backend";
        public static string ServiceVersion { get; private set; } = "0.0.1";
        public static string ServiceID { get; private set; } = null!;
        [ConfigItem(ConfigType = ConfigItemAttribute.ConfigTypeEnum.Remote)]
        public static string TraceAddress { get; private set; } = null!;
        [ConfigItem(ConfigType = ConfigItemAttribute.ConfigTypeEnum.Remote)]
        public static string LogAddress { get; private set; } = null!;
        [ConfigItem(ConfigType = ConfigItemAttribute.ConfigTypeEnum.Inject)]
        public static bool AllowUnsafeSSL { get; set; } = false;
        private static string adminAddress = null!;
        [ConfigItem(ConfigType = ConfigItemAttribute.ConfigTypeEnum.Inject,RequiredInject =true)]
        public static string AdminAddress
        {
            get => adminAddress;
            private set
            {
                if (!value.StartsWith("https://") && !value.StartsWith("http://"))
                    value = "https://" + value;
                if (!value.EndsWith("/"))
                    value += "/";
                adminAddress = value;
            }
        }
        public static string GetApiAddress(string path)
        {
            return AdminAddress + path;
        }
    }
}