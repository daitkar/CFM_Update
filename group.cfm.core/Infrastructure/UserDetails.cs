using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group.cfm.core.Infrastructure
{
    class UserDetails:IUser
    {
        public long GetUniqueId()
        {
            return 1;
        }

        public string GetUserEmail()
        {
            return "abc@gmail.com";
        }

        public string GetUserName()
        {
            return "ABC ABC";
        }
    }
}
