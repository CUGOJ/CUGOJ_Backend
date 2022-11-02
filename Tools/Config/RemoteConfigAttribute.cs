using CUGOJ.Backend.Share.Infra;
using CUGOJ.Backend.Tools.Common;
using PostSharp.Aspects;
using PostSharp.Reflection;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Tools
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class RemoteConfigAttribute : LocationInterceptionAspect
    {
        private static IConfigProvider? _configProvider;

        private long version = 0;
        private string _key = string.Empty;
        private Type targetType = typeof(string);
        
        public RemoteConfigAttribute(string key = "")
        {
            if (!string.IsNullOrEmpty(key))
                _key = key;
        }
        public static void SetConfigProvider(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }
        public override void CompileTimeInitialize(LocationInfo targetLocation, AspectInfo aspectInfo)
        {
            _key = targetLocation.PropertyInfo.Name;
            targetType = targetLocation.PropertyInfo.PropertyType;
            base.CompileTimeInitialize(targetLocation, aspectInfo);
        }

        [TimerLock]
        private async Task UpdateValue(LocationInterceptionArgs args)
        {
            var newValue = await _configProvider!.Get(_key, targetType);
            if (newValue != null)
            {
                version = _configProvider.Version;
                args.SetNewValue(newValue);
            }
        }

        public override async void OnGetValue(LocationInterceptionArgs args)
        {
           
            if(_configProvider !=null)
            {
                if(_configProvider.Version != version)
                {
                    await UpdateValue(args);
                }
            }
            
            base.OnGetValue(args);
        }
    }
}
