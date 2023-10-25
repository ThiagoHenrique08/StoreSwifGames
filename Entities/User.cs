using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiFGames.Entities
{
    internal class User
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; private set; }
        public string? Category { get; set; }
        public User(int userId, string? name, string? email, string? phone, string? password, string? category)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Phone = phone;
            Password = password;
            Category = category;
        }
    }
}
