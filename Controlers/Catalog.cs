using System.Globalization;
using System.Text;
using SwiFGames.Entities;

namespace SwiFGames.Controlers
{
    internal class Catalog
    {
        public List<Product> products = new List<Product>();


        public void AddToProductToCatalog(Product product)
        {
            products.Add(product);
        }
        public void RemoveProductToCatalog(Product product)

        {
            products.Remove(product);
        }

        public void ChangeCatalogProduct(Product product)
        {
            foreach (Product p in products)
            {
                if (p.ProductId == product.ProductId)
                {
                    p.Name = product.Name;
                    p.Description = product.Description;
                    p.Price = product.Price;
                    break;
                }
            }
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
                sb.AppendLine(product.Price.ToString("F2", CultureInfo.InvariantCulture));

                sb.Append("*************************************************************************************");
            }
            return sb.ToString();
        }

    }
}
