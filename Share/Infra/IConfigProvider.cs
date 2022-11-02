﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Share.Infra
{
    public interface IConfigProvider
    {
        Task<object?> Get(string key, Type type);
        public Task<bool> Set<T>(string key) where T : IConvertible;
        public Task<bool> IsSetEnable();
        public long Version { get; }
    }
}
