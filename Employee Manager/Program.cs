using System;
using System.Text;
using System.Globalization;

namespace Employee_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            EmployeeManager manager = new EmployeeManager();
            // Initialize sample data
            manager.InitializeSampleData();
            bool exit = false;

            while (true)
            {
                Console.WriteLine("\nEmployee Manager");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Remove Employee");
                Console.WriteLine("3. Print All Employees (with sorting options)");
                Console.WriteLine("4. Calculate Total Salary");
                Console.WriteLine("5. Update Employee Information");
                Console.WriteLine("6. Print Employees by Department");
                Console.WriteLine("7. Find Employee by ID");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddNewEmployee(manager);
                        break;
                    case 2:
                        RemoveEmployee(manager);
                        break;
                    case 3:
                        manager.PrintAllEmployees();
                        break;
                    case 4:
                        Console.WriteLine($"Total Salary: {manager.CalculateTotalSalary()}");
                        break;
                    case 5:
                        Console.Write("Enter Employee ID to update: ");
                        string empIdToUpdate = Console.ReadLine();
                        manager.UpdateEmployeeInfo(empIdToUpdate);
                        break;
                    case 6:
                        manager.PrintEmployeesByDepartment();
                        break;
                    case 7:
                        Console.Write("Enter Employee ID to find: ");
                        string empIdToFind = Console.ReadLine();
                        manager.FindAndPrintEmployeeById(empIdToFind);
                        break;
                    case 8:
                        return; // Thoát khỏi vòng lặp và chương trình
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        private static void AddNewEmployee(EmployeeManager manager)
        {
            Console.Write("Enter Employee ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Date of Birth (yyyy-MM-dd): ");
            DateTime dateOfBirth;
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            // Select Department
            Console.WriteLine("Select Department:");
            int counter = 1;
            foreach (var dept in manager.Departments)
            {
                Console.WriteLine($"{counter}. {dept.Name}");
                counter++;
            }
            int deptChoice;
            if (!int.TryParse(Console.ReadLine(), out deptChoice) || deptChoice < 1 || deptChoice > manager.Departments.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }
            Department department = manager.Departments[deptChoice - 1];

            // Select Position
            Console.WriteLine("Select Position:");
            counter = 1;
            foreach (var pos in manager.Positions)
            {
                Console.WriteLine($"{counter}. {pos.Title}");
                counter++;
            }
            int posChoice;
            if (!int.TryParse(Console.ReadLine(), out posChoice) || posChoice < 1 || posChoice > manager.Positions.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }
            Position position = manager.Positions[posChoice - 1];

            // Create new employee
            Employee newEmployee = new Employee(id, name, dateOfBirth, department, position);
            manager.AddEmployee(newEmployee);
            Console.WriteLine("Employee added successfully.");
        }
        private static void RemoveEmployee(EmployeeManager manager)
        {
            Console.Write("Enter Employee ID to remove: ");
            string removeId = 
                Console.ReadLine();

            manager.RemoveEmployee(removeId);
            Console.WriteLine("Employee removed successfully.");
        }

        // Các phương thức khác nếu cần
    }
}
