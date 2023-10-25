using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiFGames.Entities
{
    internal class Administrator : User
    {
        public Administrator(int userId, string name, string email, string phone, string password, string category)
              : base(userId, name, email, phone, password, category)
        {
        }
    }

}
