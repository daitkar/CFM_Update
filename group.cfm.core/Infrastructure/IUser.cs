using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group.cfm.core
{
    public interface IUser
    {
        long GetUniqueId();
        string GetUserEmail();
        string GetUserName();
    }
}
