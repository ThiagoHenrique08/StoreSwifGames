using System.Security.Cryptography.X509Certificates;

namespace SwiFGames.Entities
{
    internal class Customer : User
    {
        public Customer(int id, string name, string email, string phone, string password, string category) 
            : base(id, name, email, phone, password, category)
        {
        }
    }
 
}
