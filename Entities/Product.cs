using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiFGames.Entities
{
     public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price {  get; set; }

        public int Quantity { get; set; }

        public Product()
        {
        }

        public Product(int productId, string? name, string? description, double price, int quantity)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
    }
}
