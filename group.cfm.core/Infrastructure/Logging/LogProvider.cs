using group.cfm.core.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace group.cfm.core.Infrastructure.Logging
{
    /// <summary>
    /// Provides a mechanism to create instances of <see cref="ILog" /> objects.
    /// </summary>
    public static class LogProvider
    {
        /// <summary>
        /// The disable logging environment variable. If the environment variable is set to 'true', then logging
        /// will be disabled.
        /// </summary>
        public const string DisableLoggingEnvironmentVariable = "$rootnamespace$_LIBLOG_DISABLE";
        private const string NullLogProvider = "Current Log Provider is not set. Call SetCurrentLogProvider " +
                                               "with a non-null value first.";

        static LogProvider()
        {
            IsDisabled = false;
        }

        /// <summary>
        /// Sets the current log provider.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        public static void SetCurrentLogProvider(ILogProvider logProvider)
        {
            CurrentLogProvider = logProvider;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is logging is disabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if logging is disabled; otherwise, <c>false</c>.
        /// </value>
        public static bool IsDisabled { get; set; }

        /// <summary>
        /// Sets an action that is invoked when a consumer of your library has called SetCurrentLogProvider. It is 
        /// important that hook into this if you are using child libraries (especially ilmerged ones) that are using
        /// LibLog (or other logging abstraction) so you adapt and delegate to them.
        /// <see cref="SetCurrentLogProvider"/> 
        /// </summary>
        internal static ILogProvider CurrentLogProvider { get; private set; }

        /// <summary>
        /// Gets a logger for the specified type.
        /// </summary>
        /// <typeparam name="T">The type whose name will be used for the logger.</typeparam>
        /// <returns>An instance of <see cref="ILog"/></returns>
        public static ILog For<T>()
        {
            return GetLogger(typeof(T));
        }

        /// <summary>
        /// Gets a logger for the current class.
        /// </summary>
        /// <returns>An instance of <see cref="ILog"/></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ILog GetCurrentClassLogger()
        {
            var stackFrame = new StackFrame(1, false);
            return GetLogger(stackFrame.GetMethod().DeclaringType);
        }

        /// <summary>
        /// Gets a logger for the specified type.
        /// </summary>
        /// <param name="type">The type whose name will be used for the logger.</param>
        /// <returns>An instance of <see cref="ILog"/></returns>
        public static ILog GetLogger(Type type)
        {
            return GetLogger(type.FullName);
        }

        /// <summary>
        /// Gets a logger with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>An instance of <see cref="ILog"/></returns>
        public static ILog GetLogger(string name)
        {
            var logProvider = CurrentLogProvider;
            return logProvider == null
                ? NoOpLogger.Instance
                : (ILog)new LoggerExecutionWrapper(logProvider.GetLogger(name), () => IsDisabled);
        }

        internal class NoOpLogger : ILog
        {
            internal static readonly NoOpLogger Instance = new NoOpLogger();

            public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception, params object[] formatParameters)
            {
                return false;
            }
        }
    }

    internal class LoggerExecutionWrapper : ILog
    {
        private readonly ILog logger;
        private readonly Func<bool> getIsDisabled;
        internal const string FailedToGenerateLogMessage = "Failed to generate log message";

        internal LoggerExecutionWrapper(ILog logger, Func<bool> getIsDisabled = null)
        {
            this.logger = logger;
            this.getIsDisabled = getIsDisabled ?? (() => false);
        }

        internal ILog WrappedLogger
        {
            get { return logger; }
        }

        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            if (getIsDisabled())
            {
                return false;
            }

            var envVar = Environment.GetEnvironmentVariable(LogProvider.DisableLoggingEnvironmentVariable);

            if (envVar != null && envVar.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (messageFunc == null)
            {
                return logger.Log(logLevel, null);
            }

            Func<string> wrappedMessageFunc = () =>
            {
                try
                {
                    return messageFunc();
                }
                catch (Exception ex)
                {
                    Log(LogLevel.Error, () => FailedToGenerateLogMessage, ex);
                }
                return null;
            };
            return logger.Log(logLevel, wrappedMessageFunc, exception, formatParameters);
        }
    }
}
