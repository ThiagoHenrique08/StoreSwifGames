using System.Globalization;
using System.Text;

namespace SwiFGames.Entities
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

        public void ChangeCatalogProduct(int id, string name, string description, double price)
        {
            foreach (Product p in products)
            {
                if (p.ProductId == id)
                {
                    p.Name = name;
                    p.Description = description;
                    p.Price = price;
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
