using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group.cfm.core.Infrastructure
{
    public static  class UserFactory
    {
        /// <summary>
        /// Get user with specific Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IUser GetUserDetails(long id)
        {
            IUser user = null;

            user = new UserDetails();

            return user;
        }
        /// <summary>
        /// Get the current user details
        /// </summary>
        /// <returns></returns>
        public static IUser CurrentUserDetails()
        {
            IUser user = null;

            user = new UserDetails();

            return user;
        }
    }
}
