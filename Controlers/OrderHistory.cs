using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwiFGames.Entities;

namespace SwiFGames.Controlers
{
    internal class OrderHistory
    {
        public List<Order> orders { get; set; } = new List<Order>();



        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            orders.Remove(order);
        }

        public override string ToString()
        {
            double valorTotalPedido = 0.00;
            StringBuilder sb = new StringBuilder();
            foreach (Order order in orders)
            {
                

                sb.Append("Id: ");
                sb.Append(order.OrderId);
                sb.AppendLine();
                sb.Append("Data do pedido: ");
                sb.Append(order.Moment);
                sb.AppendLine();
                sb.Append("Status: ");
                sb.Append(order.Status);
                sb.AppendLine();
                sb.Append("Cliente: ");
                sb.Append(order.Customer.Name);
                sb.AppendLine();
                sb.Append("**********************");
                sb.Append("PRODUTOS NO CARRINHO: ");
                sb.Append("**********************");
                sb.AppendLine();
                foreach (Product product in order.Products)
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
                    valorTotalPedido += product.Price;
                }
                sb.AppendLine();
                sb.Append("Valor total do pedido: ");
                sb.AppendLine(valorTotalPedido.ToString(CultureInfo.InvariantCulture));
                sb.Append("===========================================================================");
                sb.AppendLine();
            }
                

            return sb.ToString();
        }

    }
}
