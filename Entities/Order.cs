using SwiFGames.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiFGames.Entities
{
    internal class Order
    {
        public DateTime Moment { get; set; }
        public Status Status { get; set; }
        public Customer? Customer { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public Order()
        {   
        }
        public Order(DateTime moment, Status status, Customer? customer)
        {
            Moment = moment;
            Status = status;
            Customer = customer;
        }

        void AddProductToTheOrder(Product product)
        {
            Products.Add(product);
        }
        void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
    }
}
