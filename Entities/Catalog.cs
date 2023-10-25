using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiFGames.Entities
{
    internal class Catalog
    {
        public List<Product> products = new List<Product>();
        public void AddToTheCatalog(Product product)
        {
            products.Add(product);
        }
        public void RemoveTheCatalog(Product product)
        {
            products.Remove(product);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("*************************************************************************************");
            foreach (Product product in products)
            {
                sb.AppendLine();
                sb.Append("Id: ");
                sb.AppendLine(product.ProductId.ToString());
                sb.Append("Name: ");
                sb.AppendLine(product.Name);
                sb.Append("Description: ");
                sb.AppendLine(product.Description);
                sb.Append("Price: ");
                sb.AppendLine(product.Price.ToString("F2",CultureInfo.InvariantCulture));

               sb.Append("*************************************************************************************");
            }
            return sb.ToString();
        }

    }
}
