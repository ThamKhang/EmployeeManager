using System;

namespace Employee_Manager
{
    public class Employee
    {
        // Properties
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Department Department { get; set; }
        public Position Position { get; set; }

        // Constructor
        public Employee(string id, string name, DateTime dateOfBirth, Department department, Position position)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Department = department;
            Position = position;
        }

        // Method to calculate salary based on position allowance
        public double CalculateSalary()
        {
            double baseSalary = 10000000; // Base salary for all employees
            double allowance = Position != null ? Position.Allowance : 0; // Ensure Position is not null
            return baseSalary * (1 + allowance);
        }
    }
}
