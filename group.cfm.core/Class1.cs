using group.cfm.core.Infrastructure;
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
    public class Class1
    {
        private readonly ILog logger;
        public Class1()
        {
            LogProvider.SetCurrentLogProvider(new Provider());
            logger = LogProvider.For<Class1>();
        }
        
        public void GetUserDetails()
        {
            IUser user = UserFactory.GetUserDetails(1);
            long userId =  user.GetUniqueId();
            string email = user.GetUserEmail();
            string userName = user.GetUserName();
            
            string message = "Hello";
            logger.InfoFormat("Importing:\n{@message}\n", message);
        }
    }
}
