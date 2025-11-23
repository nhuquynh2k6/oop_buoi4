using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    abstract class Employee
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime HireDate { get; set; }
        public string Department { get; set; }

        public Employee(string id, string name, DateTime hireDate, string dept)
        {
            EmployeeId = id;
            FullName = name;
            HireDate = hireDate;
            Department = dept;
        }

        public abstract double CalculateSalary();

        public virtual string GetInfo()
        {
            return $"[{EmployeeId}] {FullName} - {Department} - Lương: {CalculateSalary():N0} VNĐ";
        }
    }

    class FullTimeEmployee : Employee
    {
        public double BaseSalary { get; set; }
        public double BonusRate { get; set; }

        public FullTimeEmployee(string id, string name, DateTime hireDate, string dept, double baseSalary, double bonusRate) : base(id, name, hireDate, dept)
        {
            BaseSalary = baseSalary;
            BonusRate = Math.Max(0, Math.Min(0.3, bonusRate));
        }

        public override double CalculateSalary()
        {
            return BaseSalary * (1 + BonusRate);
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" (Chính thức, thưởng {BonusRate:P0})";
        }
    }

    class PartTimeEmployee : Employee
    {
        public double HourlyRate { get; set; }
        public int HoursWorked { get; set; }

        public PartTimeEmployee(string id, string name, DateTime hireDate, string dept, double hourlyRate, int hoursWorked) : base(id, name, hireDate, dept)
        {
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        }

        public override double CalculateSalary()
        {
            return HourlyRate * HoursWorked;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" (Thời vụ, {HoursWorked}h)";
        }
    }

    class Manager : FullTimeEmployee
    {
        public int NumberOfSubordinates { get; set; }

        public Manager(string id, string name, DateTime hireDate, string dept, double baseSalary, double bonusRate, int subordinates) : base(id, name, hireDate, dept, baseSalary, bonusRate)
        {
            NumberOfSubordinates = subordinates;
        }

        public override double CalculateSalary()
        {
            double basePay = base.CalculateSalary();
            int allowance = (NumberOfSubordinates / 5) * 500000;
            return basePay + allowance;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" (Quản lý {NumberOfSubordinates} người)";
        }
    }
}

