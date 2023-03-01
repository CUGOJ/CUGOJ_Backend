using CUGOJ.Tools.Infra;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Tools.Common
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.Method)]
    public class ReaderWriterLockAttribute : OnMethodBoundaryAspect
    {
        public enum LockTypeEnum
        {
            Read,
            Write
        }

        private static InstanceHashTable<ReaderWriterLockSlim> locks = new();
        private LockTypeEnum _lockType = LockTypeEnum.Write;
        public int ReaderLockTimeOut { get; set; } = -1;
        public int WriterLockTimeOut { get; set; } = -1;
        public ReaderWriterLockAttribute(LockTypeEnum lockType)
        {
            _lockType = lockType;
        }
        public override void OnEntry(MethodExecutionArgs args)
        {
            var _lock = locks.GetItem(args.Instance);
            if (_lock == null)
            {
                lock (args.Instance)
                {
                    _lock = locks.GetItem(args.Instance);
                    if (_lock == null)
                    {
                        _lock = new ReaderWriterLockSlim();
                        locks.PushItem(args.Instance, _lock);
                    }
                }
            }
            if (_lockType == LockTypeEnum.Read)
            {
                var timeout = ReaderLockTimeOut;
                if (timeout == -1)
                    timeout = Config.DefaultReaderLockTimeout;
                if (!_lock.TryEnterReadLock(timeout))
                    throw new Exception("服务繁忙,请稍后再试");
            }
            else
            {
                var timeout = WriterLockTimeOut;
                if (timeout == -1)
                    timeout = Config.DefaultWriterLockTimeout;
                if (!_lock.TryEnterWriteLock(timeout))
                    throw new Exception("服务繁忙,请稍后再试");
            }
            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var _lock = locks.GetItem(args.Instance);
            if (_lock != null) 
            {
                if (_lockType == LockTypeEnum.Read)
                    _lock.ExitReadLock();
                else
                    _lock.ExitWriteLock();
            }
            base.OnExit(args);
        }
    }
}
