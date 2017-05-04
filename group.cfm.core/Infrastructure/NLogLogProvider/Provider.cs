using LogLevel = group.cfm.core.Infrastructure.Contracts.LogLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using group.cfm.core.Infrastructure.Logging;
using group.cfm.core.Infrastructure.Contracts;

namespace group.cfm.core.Infrastructure.NLogLogProvider
{
    class Provider : LogProviderBase
    {

        public override ILog GetLogger(string name)
        {
            return new NLogLogger(LogManager.GetLogger(name));
        }

        protected override OpenNdc GetOpenNdcMethod()
        {
            return NestedDiagnosticsContext.Push;
        }

        protected override OpenMdc GetOpenMdcMethod()
        {
            return (key, value) =>
            {
                MappedDiagnosticsContext.Set(key, value);
                return new DisposableAction(() => MappedDiagnosticsContext.Remove(key));
            };
        }

        internal class NLogLogger : ILog
        {
            private readonly Logger logger;

            internal NLogLogger(Logger logger)
            {
                this.logger = logger;
            }

            public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception, params object[] formatParameters)
            {
                if (messageFunc == null)
                {
                    return IsLogLevelEnable(logLevel);
                }
                messageFunc = LogMessageFormatter.SimulateStructuredLogging(messageFunc, formatParameters);

                if (exception != null)
                {
                    return LogException(logLevel, messageFunc, exception);
                }
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        if (logger.IsDebugEnabled)
                        {
                            logger.Debug(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Info:
                        if (logger.IsInfoEnabled)
                        {
                            logger.Info(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Warn:
                        if (logger.IsWarnEnabled)
                        {
                            logger.Warn(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Error:
                        if (logger.IsErrorEnabled)
                        {
                            logger.Error(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Fatal:
                        if (logger.IsFatalEnabled)
                        {
                            logger.Fatal(messageFunc());
                            return true;
                        }
                        break;
                    default:
                        if (logger.IsTraceEnabled)
                        {
                            logger.Trace(messageFunc());
                            return true;
                        }
                        break;
                }
                return false;
            }

            private bool LogException(LogLevel logLevel, Func<string> messageFunc, Exception exception)
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        if (logger.IsDebugEnabled)
                        {
                            logger.Debug(exception, messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Info:
                        if (logger.IsInfoEnabled)
                        {
                            logger.Info(exception, messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Warn:
                        if (logger.IsWarnEnabled)
                        {
                            logger.Warn(exception, messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Error:
                        if (logger.IsErrorEnabled)
                        {
                            logger.Error(exception, messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Fatal:
                        if (logger.IsFatalEnabled)
                        {
                            logger.Fatal(exception, messageFunc());
                            return true;
                        }
                        break;
                    default:
                        if (logger.IsTraceEnabled)
                        {
                            logger.Trace(exception, messageFunc());
                            return true;
                        }
                        break;
                }
                return false;
            }

            private bool IsLogLevelEnable(LogLevel logLevel)
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        return logger.IsDebugEnabled;
                    case LogLevel.Info:
                        return logger.IsInfoEnabled;
                    case LogLevel.Warn:
                        return logger.IsWarnEnabled;
                    case LogLevel.Error:
                        return logger.IsErrorEnabled;
                    case LogLevel.Fatal:
                        return logger.IsFatalEnabled;
                    default:
                        return logger.IsTraceEnabled;
                }
            }
        }
    }
}
