using group.cfm.core.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading.Tasks;

namespace group.cfm.core.Infrastructure.Logging
{
    public abstract class LogProviderBase : ILogProvider
    {
        protected delegate IDisposable OpenNdc(string message);
        protected delegate IDisposable OpenMdc(string key, string value);

        private readonly Lazy<OpenNdc> lazyOpenNdcMethod;
        private readonly Lazy<OpenMdc> lazyOpenMdcMethod;
        private static readonly IDisposable NoopDisposableInstance = new DisposableAction();

        protected LogProviderBase()
        {
            lazyOpenNdcMethod
                = new Lazy<OpenNdc>(GetOpenNdcMethod);
            lazyOpenMdcMethod
               = new Lazy<OpenMdc>(GetOpenMdcMethod);
        }

        public abstract ILog GetLogger(string name);

        public IDisposable OpenNestedContext(string message)
        {
            return lazyOpenNdcMethod.Value(message);
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            return lazyOpenMdcMethod.Value(key, value);
        }

        protected virtual OpenNdc GetOpenNdcMethod()
        {
            return _ => NoopDisposableInstance;
        }

        protected virtual OpenMdc GetOpenMdcMethod()
        {
            return (_, __) => NoopDisposableInstance;
        }
    }

    public class DisposableAction : IDisposable
    {
        private readonly Action onDispose;

        public DisposableAction(Action onDispose = null)
        {
            this.onDispose = onDispose;
        }

        public void Dispose()
        {
            if (onDispose != null)
            {
                onDispose();
            }
        }
    }

    public static class LogMessageFormatter
    {
        private static readonly Regex Pattern = new Regex(@"\{@?\w{1,}\}");

        /// <summary>
        /// Some logging frameworks support structured logging, such as serilog. This will allow you to add names to structured data in a format string:
        /// For example: Log("Log message to {user}", user). This only works with serilog, but as the user of LibLog, you don't know if serilog is actually 
        /// used. So, this class simulates that. it will replace any text in {curly braces} with an index number. 
        /// 
        /// "Log {message} to {user}" would turn into => "Log {0} to {1}". Then the format parameters are handled using regular .net string.Format.
        /// </summary>
        /// <param name="messageBuilder">The message builder.</param>
        /// <param name="formatParameters">The format parameters.</param>
        /// <returns></returns>
        public static Func<string> SimulateStructuredLogging(Func<string> messageBuilder, object[] formatParameters)
        {
            if (formatParameters == null || formatParameters.Length == 0)
            {
                return messageBuilder;
            }

            return () =>
            {
                string targetMessage = messageBuilder();
                int argumentIndex = 0;
                foreach (Match match in Pattern.Matches(targetMessage))
                {
                    int notUsed;
                    if (!int.TryParse(match.Value.Substring(1, match.Value.Length - 2), out notUsed))
                    {
                        targetMessage = ReplaceFirst(targetMessage, match.Value,
                            "{" + argumentIndex++ + "}");
                    }
                }
                try
                {
                    return string.Format(CultureInfo.InvariantCulture, targetMessage, formatParameters);
                }
                catch (FormatException ex)
                {
                    throw new FormatException("The input string '" + targetMessage + "' could not be formatted using string.Format", ex);
                }
            };
        }

        private static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search, StringComparison.Ordinal);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }
}
