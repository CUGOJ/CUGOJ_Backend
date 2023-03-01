using CUGOJ.Tools.Common;
using CUGOJ.Share.Infra;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PostSharp.Aspects;
using PostSharp.Aspects.Configuration;
using PostSharp.Extensibility;
using PostSharp.Reflection;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Tools
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.Property)]
    [MulticastAttributeUsage(PersistMetaData = true)]
    public class ConfigItemAttribute : LocationInterceptionAspect
    {
        public enum ConfigTypeEnum
        {
            // 允许远程配置中心加载
            Remote = 0x01,
            // 允许启动参数注入
            Inject = 0x02,
            // 允许修改
            Editable = 0x04,
            // 如果没有被修改或参数注入,则允许从配置中心加载,如果和Remote一起设置会覆盖掉Remote选项
            RemoteIfNotEdit = 0x08,

            All = Remote|Inject|Editable
        }
        public ConfigTypeEnum ConfigType { get; set; }
        public bool CanInject => (ConfigType & ConfigTypeEnum.Inject) != 0;
        public bool CanEdit => (ConfigType & ConfigTypeEnum.Editable) != 0;
        public bool CanRemote => (ConfigType & ConfigTypeEnum.RemoteIfNotEdit) != 0 && !Edited ||
            (ConfigType & ConfigTypeEnum.RemoteIfNotEdit) == 0 && (ConfigType & ConfigTypeEnum.Remote) != 0;
        public bool Edited { get; private set; } = false;
        public static bool Injected { get => _injected; set => _injected = true; }
        private static bool _injected = false;
        private static IConfigProvider? _configProvider;
        public static IConfigProvider? ConfigProvider
        {
            get => _configProvider;
            set
            {
                _configProvider ??= value;
            }
        }

        private long version = 0;
        private string? key;
        private string? notSetMsg;
        private string? notInjectMsg;
        public string PropertyName { get; set; } = string.Empty;
        public string Key
        {
            get => key ?? PropertyName;
            set => key = value;
        }

        public Type TargetType { get; set; } = typeof(string);

        /// <summary>
        /// 是否必须,可以通过启动参数注入或通过远程配置中心获取
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// 是否必须参数注入
        /// </summary>
        public bool RequiredInject { get; set; }
        /// <summary>
        /// Required为true的情况下没有设置的提示语
        /// </summary>
        public string? NotSetMsg
        {
            get => notSetMsg ?? $"{Key} 配置为必填项,但是没有被设置,请使用-{Key}来注入配置,或在配置中心配置{Config.Env}环境的配置,配置类型为:{TargetType.Name}";
            set => notSetMsg = value;
        }
        /// <summary>
        /// Required为true的情况下没有设置的提示语
        /// </summary>
        public string? NotInjectMsg
        {
            get => notInjectMsg ?? $"{key} 配置必须在参数中注入,但是没有找到,请使用-{key}来注入配置,配置类型为:{TargetType.Name}";
            set => notInjectMsg = value;
        }

        public override void CompileTimeInitialize(LocationInfo targetLocation, AspectInfo aspectInfo)
        {
            PropertyName = targetLocation.PropertyInfo.Name;
            TargetType = targetLocation.PropertyInfo.PropertyType;
            base.CompileTimeInitialize(targetLocation, aspectInfo);
        }

        public override void RuntimeInitialize(LocationInfo locationInfo)
        {
            PropertyName = locationInfo.PropertyInfo.Name;
            TargetType = locationInfo.PropertyInfo.PropertyType;
            base.RuntimeInitialize(locationInfo);
        }
        [TimerLock]
        private async Task UpdateValue(LocationInterceptionArgs args)
        {
            var newValue = await _configProvider!.Get(Key, TargetType);
            if (newValue != null)
            {
                version = _configProvider.Version;
                args.SetNewValue(newValue);
            }
        }

        public override async void OnGetValue(LocationInterceptionArgs args)
        {
            if (CanRemote && _configProvider != null)
            {
                if (_configProvider.Version != version)
                {
                    await UpdateValue(args);
                }
            }

            base.OnGetValue(args);
        }
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            if ((CanInject || CanRemote) && !Injected || CanEdit)
            {
                Edited = true;
                base.OnSetValue(args);
            }
        }
    }
}
