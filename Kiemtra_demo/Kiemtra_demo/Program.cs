using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Company company = new Company();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== QUẢN LÝ NHÂN VIÊN ===");
                Console.WriteLine("1. Thêm nhân viên (đã có mẫu)");
                Console.WriteLine("2. Hiển thị tất cả nhân viên");
                Console.WriteLine("3. Tổng quỹ lương");
                Console.WriteLine("4. Thống kê theo loại");
                Console.WriteLine("5. Nhân viên theo phòng ban");
                Console.WriteLine("6. Top 5 lương cao nhất");
                Console.WriteLine("7. Sắp xếp theo lương giảm dần");
                Console.WriteLine("8. Thoát");
                Console.Write("Chọn: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        InitializeSampleData(company);
                        break;
                    case "2":
                        DisplayAll(company);
                        break;
                    case "3":
                        Console.WriteLine($"Tổng quỹ lương: {company.CalculateTotalPayroll():N0} VNĐ");
                        break;
                    case "4":
                        DisplayCountByType(company);
                        break;
                    case "5":
                        Console.Write("Nhập phòng ban: ");
                        string dept = Console.ReadLine();
                        DisplayList(company.GetEmployeesByDepartment(dept));
                        break;
                    case "6":
                        //DisplayList(company.GetTopHighestPaid(5));
                        break;
                    case "7":
                        company.SortEmployeesBySalaryDescending();
                        DisplayAll(company);
                        break;
                    case "8":
                        return;
                }
                Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
            }
        }

        static void DisplayAll(Company company)
        {
            foreach (var e in company.Employees)
                Console.WriteLine(e.GetInfo());
        }

        static void DisplayList(List<Employee> list)
        {
            if (list.Count == 0)
                Console.WriteLine("Không tìm thấy.");
            else
                foreach (var e in list)
                    Console.WriteLine(e.GetInfo());
        }

        static void DisplayCountByType(Company company)
        {
            var counts = company.CountEmployeesByType();
            foreach (var kvp in counts)
                Console.WriteLine($"{kvp.Key}: {kvp.Value} người");
        }

        static void InitializeSampleData(Company company)
        {
            company.AddEmployee(new FullTimeEmployee("FT001", "Nguyễn Văn An", new DateTime(2020, 1, 15), "IT", 15000000, 0.1));
            company.AddEmployee(new FullTimeEmployee("FT002", "Trần Thị Bé", new DateTime(2019, 6, 20), "HR", 12000000, 0.15));
            company.AddEmployee(new FullTimeEmployee("FT003", "Lê Văn Cung", new DateTime(2021, 3, 10), "IT", 18000000, 0.2));
            company.AddEmployee(new FullTimeEmployee("FT004", "Phạm Thị Dương", new DateTime(2022, 5, 5), "Marketing", 13000000, 0.05));

            company.AddEmployee(new PartTimeEmployee("PT001", "Hoàng Văn Thạch", new DateTime(2023, 1, 1), "IT", 120000, 80));
            company.AddEmployee(new PartTimeEmployee("PT002", "Ngô Thị Phương", new DateTime(2023, 2, 15), "HR", 100000, 60));
            company.AddEmployee(new PartTimeEmployee("PT003", "Vũ Văn Giang", new DateTime(2023, 3, 20), "IT", 150000, 100));

            company.AddEmployee(new Manager("MG001", "Đỗ Thị Hường", new DateTime(2018, 4, 1), "IT", 25000000, 0.25, 12));
            company.AddEmployee(new Manager("MG002", "Bùi Văn Khương", new DateTime(2017, 7, 10), "HR", 22000000, 0.2, 8));
            company.AddEmployee(new Manager("MG003", "Mai Văn Mới", new DateTime(2019, 9, 15), "Marketing", 20000000, 0.18, 15));
        }
    }
}

