using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exploration3.Models
{
    public class CurrentUser
    {
        public static UserModel user = null;
        private CurrentUser()
        {

        }
        public void SetUser(UserModel userModel)
        {
            user = userModel;
        }
        public UserModel GetUser()
        {
            return user;
        }
    }
}
