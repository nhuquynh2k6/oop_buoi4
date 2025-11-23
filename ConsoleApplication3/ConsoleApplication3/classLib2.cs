using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }

        public Product(string id, string name, double price, int stock)
        {
            ProductId = id;
            Name = name;
            Price = price;
            StockQuantity = stock;
        }

        public void ReduceStock(int qty)
        {
            if (qty <= StockQuantity)
                StockQuantity -= qty;
        }

        public void IncreaseStock(int qty)
        {
            StockQuantity += qty;
        }

        public virtual string GetInfo()
        {
            return $"[{ProductId}] {Name} - Giá: {Price:N0} - Tồn: {StockQuantity}";
        }
    }

    class Laptop : Product
    {
        public string CPU { get; set; }
        public int RAM { get; set; }
        public string GPU { get; set; }

        public Laptop(string id, string name, double price, int stock, string cpu, int ram, string gpu)
            : base(id, name, price, stock)
        {
            CPU = cpu;
            RAM = ram;
            GPU = gpu;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" | {CPU}, {RAM}GB, {GPU}";
        }
    }

    class Smartphone : Product
    {
        public string ScreenSize { get; set; }
        public int CameraMP { get; set; }

        public Smartphone(string id, string name, double price, int stock, string screen, int camera)
            : base(id, name, price, stock)
        {
            ScreenSize = screen;
            CameraMP = camera;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" | {ScreenSize}\", {CameraMP}MP";
        }
    }

    class OrderItem
    {
        public Product Item { get; set; }
        public int Quantity { get; set; }

        public OrderItem(Product product, int quantity)
        {
            Item = product; Quantity = quantity;
        }

        public double GetSubTotal()
        {
            return Item.Price * Quantity;
        }
    }

    class Order
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public double Discount { get; set; } // %

        public Order(string id, string customer, double discount = 0)
        {
            OrderId = id;
            CustomerName = customer;
            Discount = discount;
            OrderDate = DateTime.Now;
        }

        public bool AddItem(Product p, int qty)
        {
            if (p.StockQuantity < qty)
                return false;
            Items.Add(new OrderItem(p, qty));
            p.ReduceStock(qty);
            return true;
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in Items)
                total += item.GetSubTotal();
            return total * (1 - Discount / 100);
        }

        public string GetOrderDetail()
        {
            string s = $"Đơn {OrderId} - {CustomerName} - {OrderDate:dd/MM/yyyy}\n";
            foreach (var item in Items)
                s += $"  {item.Item.Name} x{item.Quantity} = {item.GetSubTotal():N0}\n";
            s += $"Tổng sau CK ({Discount}%): {CalculateTotal():N0} VNĐ";
            return s;
        }
    }
}
