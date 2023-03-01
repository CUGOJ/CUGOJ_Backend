using Microsoft.Extensions.Logging;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Serialization;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Sinks.Loki;
using Serilog.Sinks.Loki.Labels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CUGOJ.Tools.Log
{
    public class Logger : Microsoft.Extensions.Logging.ILogger
    {

        public class LogLabelProvider : ILogLabelProvider
        {
            public IList<LokiLabel> GetLabels()
            {
                return new List<LokiLabel>
                {
                    new LokiLabel("IP",Config.IP),
                    new LokiLabel("Env",Config.Env),
                    new LokiLabel("ServiceID",Config.ServiceID),
                    new LokiLabel("ServiceName",Config.ServiceName)
                };
            }
        }

        private readonly Serilog.ILogger? _serilogger;
        public Logger()
        {
            if (!string.IsNullOrEmpty(Config.LogAddress))
            {
                var config = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.LokiHttp(new NoAuthCredentials(Config.LogAddress), new LogLabelProvider());
#if DEBUG
                config.WriteTo.Console();
#endif
                _serilogger = config.CreateLogger();
            }
            else
            {
                var config = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .Enrich.FromLogContext()
              .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
#if DEBUG
                config.WriteTo.Console();
#endif
                _serilogger = config.CreateLogger();
            }
        }

        public void Info(string message,
            [CallerMemberName] string method = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            using var methodTag = LogContext.PushProperty("Method", method);
            using var filePos = LogContext.PushProperty("FilePos", $"{sourceFilePath}:{sourceLineNumber}");
            using var traceID = LogContext.PushProperty("TraceID", Activity.Current?.TraceId);
            _serilogger?.Information(message);
        }

        public void Warn(string message,
            [CallerMemberName] string method = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            using var methodTag = LogContext.PushProperty("Method", method);
            using var filePos = LogContext.PushProperty("FilePos", $"{sourceFilePath}:{sourceLineNumber}");
            using var traceID = LogContext.PushProperty("TraceID", Activity.Current?.TraceId);
            _serilogger?.Warning(message);
        }

        public void Error(string message,
            [CallerMemberName] string method = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            using var methodTag = LogContext.PushProperty("Method", method);
            using var filePos = LogContext.PushProperty("FilePos", $"{sourceFilePath}:{sourceLineNumber}");
            using var traceID = LogContext.PushProperty("TraceID", Activity.Current?.TraceId);
            _serilogger?.Error(message);
        }

        public void Exception(Exception e,
            [CallerMemberName] string method = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {

            using var methodTag = LogContext.PushProperty("Method", method);
            using var filePos = LogContext.PushProperty("FilePos", $"{sourceFilePath}:{sourceLineNumber}");
            using var traceID = LogContext.PushProperty("TraceID", Activity.Current?.TraceId);
            _serilogger?.Error($"{e.Message},{e.StackTrace}");
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (_serilogger == null) return;
            switch (logLevel)
            {
                case LogLevel.Trace:
                    _serilogger.Verbose(formatter(state, exception));
                    break;
                case LogLevel.Debug:
                    _serilogger.Debug(formatter(state, exception));
                    break;
                case LogLevel.Information:
                    _serilogger.Information(formatter(state, exception));
                    break;
                case LogLevel.Warning:
                    _serilogger.Warning(formatter(state, exception));
                    break;
                case LogLevel.Error:
                    _serilogger.Error(formatter(state, exception));
                    break;
                case LogLevel.Critical:
                    _serilogger.Fatal(formatter(state, exception));
                    break;
                case LogLevel.None:
                    break;
                default:
                    break;
            }
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            if (_serilogger == null) return false;
            return _serilogger.IsEnabled(logLevel switch
            {
                LogLevel.Information => LogEventLevel.Information,
                LogLevel.Warning => LogEventLevel.Warning,
                LogLevel.Error => LogEventLevel.Error,
                _ => LogEventLevel.Debug
            }
            );
        }

        public IDisposable BeginScope<TState>(TState state) => default!;
    }
}
