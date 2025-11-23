using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class OnlineShop
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Order> Orders { get; set; } = new List<Order>();

        public void AddProduct(Product p)
        {
            foreach (var product in Products)
                if (product.ProductId.ToUpper() == p.ProductId.ToUpper())
                    return;
            Products.Add(p);
        }

        public bool PlaceOrder(Order order)
        {
            // Kiểm tra tồn kho trước
            foreach (var item in order.Items)
            {
                if (item.Item.StockQuantity < item.Quantity)
                    return false;
            }
            Orders.Add(order);
            return true;
        }

        public Order FindOrderById(string id)
        {
            foreach (var o in Orders)
                if (o.OrderId == id)
                    return o;
            return null;
        }

        public double CalculateRevenue(DateTime from, DateTime to)
        {
            double total = 0;
            foreach (var o in Orders)
                if (o.OrderDate >= from && o.OrderDate <= to)
                    total += o.CalculateTotal();
            return total;
        }

        public List<Product> GetBestSellingProducts(int topCount)
        {
            var soldQty = new Dictionary<string, int>();
            foreach (var o in Orders)
                foreach (var item in o.Items)
                {
                    if (soldQty.ContainsKey(item.Item.ProductId))
                        soldQty[item.Item.ProductId] += item.Quantity;
                    else
                        soldQty[item.Item.ProductId] = item.Quantity;
                }

            var list = new List<Tuple<Product, int>>();
            foreach (var p in Products)
            {
                int qty = soldQty.ContainsKey(p.ProductId) ? soldQty[p.ProductId] : 0;
                list.Add(Tuple.Create(p, qty));
            }

            list.Sort((a, b) => b.Item2.CompareTo(a.Item2));
            var result = new List<Product>();
            for (int i = 0; i < Math.Min(topCount, list.Count); i++)
                result.Add(list[i].Item1);
            return result;
        }

        public Dictionary<string, double> RevenueByCategory()
        {
            var revenue = new Dictionary<string, double>();
            revenue["Laptop"] = 0;
            revenue["Smartphone"] = 0;

            foreach (var o in Orders)
            {
                foreach (var item in o.Items)
                {
                    double amount = item.GetSubTotal() * (1 - o.Discount / 100);
                    string cat = item.Item is Laptop ? "Laptop" : "Smartphone";
                    revenue[cat] += amount;
                }
            }
            return revenue;
        }

        public List<Order> GetOrdersByCustomerName(string name)
        {
            var result = new List<Order>();
            foreach (var o in Orders)
                if (o.CustomerName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                    result.Add(o);
            return result;
        }
    }
}
