using group.cfm.core.Infrastructure.Contracts;
using group.cfm.core.Infrastructure.Logging;
using group.cfm.core.Infrastructure.NLogLogProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group.cfm.core
{
    public class WebLog
    {
        private readonly ILog logger;

        public WebLog()
        {
            LogProvider.SetCurrentLogProvider(new Provider());
            logger = LogProvider.For<WebLog>();
        }

        public void AddToLog()
        {

        }

    }
}
