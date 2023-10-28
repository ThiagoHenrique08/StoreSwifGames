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
        public int OrderId { get; set; }
        public DateTime Moment { get; set; }
        public StatusOrder Status { get; set; }
        public Customer? Customer { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public Order()
        {   
        }
        public Order(int orderId, DateTime moment, StatusOrder status, Customer? customer)
        {
            OrderId = orderId;
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


        // fazer o To string para imprimir o pedido e seus produtos
    }
}
