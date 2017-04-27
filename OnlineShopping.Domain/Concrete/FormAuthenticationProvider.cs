using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopping.Domain.Abstract;
namespace OnlineShopping.Domain.Concrete
{
    public class FormAuthenticationProvider : IAuthentication
    {
        private readonly EFDBContext context = new EFDBContext();
        public bool Authenticate ( string username, string password )
        {
            var result = context.Users.FirstOrDefault(u => u.UserId == username &&
                                                           u.Password == password);
            if (result == null)

                return false;

            return true;
        }

        public bool Logout ( )
        {
            return true;
        }
    }
}
