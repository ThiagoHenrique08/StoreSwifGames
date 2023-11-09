
using System.Text;
using SwiFGames.Entities;

namespace SwiFGames.Controlers
{
    internal class BaseUsers
    {
        public List<User> Users { get; set; } = new List<User>();
        public void AddNewUserAtBase(User user)
        {
            Users.Add(user);
        }
        public void RemoveUserAtBase(User user)
        {
            Users.Remove(user);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("*********************************");
            foreach (User user in Users)
            {
                sb.AppendLine();
                sb.Append("Id: ");
                sb.AppendLine(user.UserId.ToString());
                sb.Append("Name: ");
                sb.AppendLine(user.Name);
                sb.Append("E-mail: ");
                sb.AppendLine(user.Email);
                sb.Append("Phone: ");
                sb.AppendLine(user.Phone);
                sb.Append("Category: ");
                sb.AppendLine(user.Category);
                sb.Append("*********************************");
            }
            return sb.ToString();
        }

    }
}
