using System.Collections.Generic;

namespace Employee_Manager
{
    public class Department
    {
        // Properties
        public string Id { get; set; }
        public string Name { get; set; }
        public Employee Manager { get; set; } // Trưởng phòng
        public List<Employee> Employees { get; set; } // Danh sách nhân viên trong phòng

        // Constructor
        public Department(string id, string name, Employee manager)
        {
            Id = id;
            Name = name;
            Manager = manager;
            Employees = new List<Employee>();
        }

        // Method to add an employee to the department
        public void AddEmployee(Employee employee)
        {
            if (employee != null && !Employees.Contains(employee))
            {
                Employees.Add(employee);
                employee.Department = this; // Set the department of the employee
            }
        }

        // Method to remove an employee from the department
        public void RemoveEmployee(Employee employee)
        {
            if (employee != null && Employees.Contains(employee))
            {
                Employees.Remove(employee);
                if (employee.Department == this)
                {
                    employee.Department = null; // Clear the department of the employee
                }
            }
        }
    }
}
