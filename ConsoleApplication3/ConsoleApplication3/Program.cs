using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static OnlineShop shop = new OnlineShop();

        static void Main(string[] args)
        {
            InitializeSampleData();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== CỬA HÀNG ĐIỆN TỬ ===");
                Console.WriteLine("1. Xem sản phẩm");
                Console.WriteLine("2. Tạo đơn hàng mới");
                Console.WriteLine("3. Xem tất cả đơn hàng");
                Console.WriteLine("4. Doanh thu theo khoảng thời gian");
                Console.WriteLine("5. Top 3 sản phẩm bán chạy");
                Console.WriteLine("6. Doanh thu theo danh mục");
                Console.WriteLine("7. Tìm đơn theo khách hàng");
                Console.WriteLine("8. Xem tồn kho");
                Console.WriteLine("9. Thoát");
                Console.Write("Chọn: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": DisplayProducts(); break;
                    case "2": CreateOrder(); break;
                    case "3": DisplayAllOrders(); break;
                    case "4": RevenueByDate(); break;
                    case "5": DisplayBestSelling(3); break;
                    case "6": DisplayRevenueByCategory(); break;
                    case "7": SearchByCustomer(); break;
                    case "8": DisplayStock(); break;
                    case "9": return;
                }
                Console.WriteLine("\nNhấn phím bất kỳ...");
                Console.ReadKey();
            }
        }

        static void DisplayProducts()
        {
            foreach (var p in shop.Products)
                Console.WriteLine(p.GetInfo());
        }

        static void DisplayStock()
        {
            Console.WriteLine("TỒN KHO:");
            foreach (var p in shop.Products)
                Console.WriteLine($"  {p.Name}: {p.StockQuantity} cái");
        }

        static void CreateOrder()
        {
            Console.Write("Mã đơn: ");
            string id = Console.ReadLine();
            Console.Write("Tên khách: ");
            string cust = Console.ReadLine();
            Console.Write("Chiết khấu (%): ");
            double disc = double.Parse(Console.ReadLine());
            var order = new Order(id, cust, disc);

            while (true)
            {
                Console.Write("Mã SP (Enter để kết thúc): "); string pid = Console.ReadLine();
                if (string.IsNullOrEmpty(pid))
                    break;
                var p = FindProduct(pid);
                if (p == null)
                {
                    Console.WriteLine("Không tìm thấy!");
                    continue;
                }
                Console.Write("Số lượng: ");
                int qty = int.Parse(Console.ReadLine());
                if (!order.AddItem(p, qty))
                    Console.WriteLine("Không đủ hàng!");
            }

            if (shop.PlaceOrder(order))
                Console.WriteLine("Đặt hàng thành công!");
            else
                Console.WriteLine("Đặt hàng thất bại!");
        }

        static Product FindProduct(string id)
        {
            foreach (var p in shop.Products)
                if (p.ProductId.ToUpper() == id.ToUpper())
                    return p;
            return null;
        }

        static void DisplayAllOrders()
        {
            foreach (var o in shop.Orders)
                Console.WriteLine(o.GetOrderDetail() + "\n---");
        }

        static void RevenueByDate()
        {
            Console.Write("Từ (dd/MM/yyyy): ");
            DateTime from = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
            Console.Write("Đến (dd/MM/yyyy): ");
            DateTime to = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
            Console.WriteLine($"Doanh thu: {shop.CalculateRevenue(from, to):N0} VNĐ");
        }

        static void DisplayBestSelling(int top)
        {
            var best = shop.GetBestSellingProducts(top);
            Console.WriteLine($"TOP {top} BÁN CHẠY:");
            foreach (var p in best)
            {
                int sold = 0;
                foreach (var o in shop.Orders)
                    foreach (var item in o.Items)
                        if (item.Item.ProductId == p.ProductId)
                            sold += item.Quantity;
                Console.WriteLine($"  {p.Name}: {sold} cái");
            }
        }

        static void DisplayRevenueByCategory()
        {
            var rev = shop.RevenueByCategory();
            Console.WriteLine($"Laptop: {rev["Laptop"]:N0} VNĐ");
            Console.WriteLine($"Smartphone: {rev["Smartphone"]:N0} VNĐ");
        }

        static void SearchByCustomer()
        {
            Console.Write("Tên khách: ");
            string name = Console.ReadLine();
            var orders = shop.GetOrdersByCustomerName(name);
            if (orders.Count == 0)
                Console.WriteLine("Không tìm thấy.");
            else
                foreach (var o in orders)
                    Console.WriteLine(o.GetOrderDetail());
        }

        static void InitializeSampleData()
        {
            shop.AddProduct(new Laptop("LP001", "Dell XPS 15", 35000000, 5, "i7-12700H", 16, "RTX 3050"));
            shop.AddProduct(new Laptop("LP002", "MacBook Pro 14", 55000000, 3, "M2 Pro", 16, "Integrated"));
            shop.AddProduct(new Laptop("LP003", "Asus ROG", 42000000, 4, "Ryzen 9", 32, "RTX 3070"));
            shop.AddProduct(new Laptop("LP004", "HP Spectre", 30000000, 6, "i5-1235U", 8, "Iris Xe"));

            shop.AddProduct(new Smartphone("SP001", "iPhone 15", 25000000, 10, "6.1", 48));
            shop.AddProduct(new Smartphone("SP002", "Samsung S24", 22000000, 12, "6.2", 50));
            shop.AddProduct(new Smartphone("SP003", "Xiaomi 14", 15000000, 15, "6.36", 50));
            shop.AddProduct(new Smartphone("SP004", "Oppo Reno", 12000000, 8, "6.4", 64));
        }
    }
}