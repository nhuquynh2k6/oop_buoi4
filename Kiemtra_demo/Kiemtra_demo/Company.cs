using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Company
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();

        public void AddEmployee(Employee emp)
        {
            foreach (var e in Employees)
                if (e.EmployeeId == emp.EmployeeId)
                    return;
            Employees.Add(emp);
        }

        public bool RemoveEmployee(string id)
        {
            for (int i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].EmployeeId == id)
                {
                    Employees.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public Employee FindEmployeeById(string id)
        {
            foreach (var e in Employees)
                if (e.EmployeeId == id)
                    return e;
            return null;
        }

        public double CalculateTotalPayroll()
        {
            double total = 0;
            foreach (var e in Employees)
                total += e.CalculateSalary();
            return total;
        }

        public List<Employee> GetEmployeesByDepartment(string dept)
        {
            var result = new List<Employee>();
            foreach (var e in Employees)
                if (e.Department.ToUpper() == dept.ToUpper())
                    result.Add(e);
            return result;
        }

        public void SortEmployeesBySalaryDescending()
        {
            Employee tmpEmp;
            for (int i=0;i<Employees.Count-1;i++)
                for (int j=i+1;j<Employees.Count;j++)
                    if (Employees[j].CalculateSalary()> Employees[i].CalculateSalary())
                    {
                        tmpEmp = Employees[j];
                        Employees[j] = Employees[i];
                        Employees[i] = tmpEmp;
                    }
        }

        public Dictionary<string, int> CountEmployeesByType()
        {
            var count = new Dictionary<string, int>();
            foreach (var e in Employees)
            {
                string type = e.GetType().Name;
                if (count.ContainsKey(type))
                    count[type]++;
                else
                    count[type] = 1;
            }
            return count;
        }
    }
}
