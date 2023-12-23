using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Employee_Manager
{
    public class EmployeeManager
    {
        private List<Employee> employees;
        private List<Department> departments;
        private List<Position> positions;

        public EmployeeManager()
        {
            employees = new List<Employee>();
            departments = new List<Department>();
            positions = new List<Position>();

            InitializeDepartments();
            InitializePositions();
        }

        public List<Department> Departments => departments;
        public List<Position> Positions => positions;
        private void InitializeDepartments()
        {
            // Khởi tạo các phòng ban mặc định
            departments.Add(new Department("HR", "Human Resources", null));
            departments.Add(new Department("IT", "Information Technology", null));
            // Các phòng ban khác...
        }

        private void InitializePositions()
        {
            // Khởi tạo các chức vụ mặc định
            positions.Add(new Position("Director", 0.25));
            positions.Add(new Position("Manager", 0.15));
            positions.Add(new Position("Deputy Manager", 0.05));
            positions.Add(new Position("Employee", 0.0));
            // Các chức vụ khác...
        }
        // Thêm nhân viên
        public void AddEmployee(Employee employee)
        {
            if (employee != null && !employees.Any(e => e.Id == employee.Id))
            {
                employees.Add(employee);
            }
            else
            {
                Console.WriteLine("Employee already exists or is null.");
            }
        }

        // Xóa nhân viên
        public void RemoveEmployee(string employeeId)
        {
            var employee = employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee != null)
            {
                employees.Remove(employee);
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
        // Cập nhật thông tin nhân viên
        public void UpdateEmployeeInfo(string employeeId)
        {
            var employee = employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            Console.WriteLine("Updating Employee: " + employee.Name);
            Console.Write("Enter new name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                employee.Name = newName;
            }

            Console.Write("Enter new Date of Birth (yyyy-MM-dd) or leave blank: ");
            string newDobInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDobInput))
            {
                if (DateTime.TryParseExact(newDobInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newDob))
                {
                    employee.DateOfBirth = newDob;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Keeping current date of birth.");
                }
            }

            Console.WriteLine("Select new Department or leave blank:");
            int counter = 1;
            foreach (var dept in Departments)
            {
                Console.WriteLine($"{counter}. {dept.Name}");
                counter++;
            }
            string deptChoice = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(deptChoice) && int.TryParse(deptChoice, out int deptIndex) && deptIndex >= 1 && deptIndex <= Departments.Count)
            {
                employee.Department = Departments[deptIndex - 1];
            }

            Console.WriteLine("Select new Position or leave blank:");
            counter = 1;
            foreach (var pos in Positions)
            {
                Console.WriteLine($"{counter}. {pos.Title}");
                counter++;
            }
            string posChoice = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(posChoice) && int.TryParse(posChoice, out int posIndex) && posIndex >= 1 && posIndex <= Positions.Count)
            {
                employee.Position = Positions[posIndex - 1];
            }

            Console.WriteLine("Employee updated successfully.");
        }

        // Tìm kiếm nhân viên
        public Employee FindEmployee(string employeeId)
        {
            return employees.FirstOrDefault(e => e.Id == employeeId);
        }

        // Thêm phòng ban
        public void AddDepartment(Department department)
        {
            departments.Add(department);
        }

        // Xóa phòng ban
        public void RemoveDepartment(string departmentId)
        {
            departments.RemoveAll(d => d.Id == departmentId);
        }

        // Cập nhật thông tin phòng ban
        public void UpdateDepartment(Department department)
        {
            var index = departments.FindIndex(d => d.Id == department.Id);
            if (index != -1)
            {
                departments[index] = department;
            }
        }

        // Tìm kiếm phòng ban
        public Department FindDepartment(string departmentId)
        {
            return departments.FirstOrDefault(d => d.Id == departmentId);
        }

        // Tính toán tổng lương
        public double CalculateTotalSalary()
        {
            double totalSalary = 0;
            foreach (var employee in employees)
            {
                totalSalary += employee.CalculateSalary();
            }
            return totalSalary;
        }
        public void SortEmployeesByName()
        {
            employees.Sort((e1, e2) => e1.Name.CompareTo(e2.Name));
        }

        public void PrintAllEmployees()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees to display.");
                return;
            }

            Console.WriteLine("Sort by: ");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Department");
            Console.WriteLine("3. Position");
            Console.WriteLine("4. No Sorting");
            Console.Write("Choose your option: ");
            int sortOption;
            if (!int.TryParse(Console.ReadLine(), out sortOption) || sortOption < 1 || sortOption > 4)
            {
                Console.WriteLine("Invalid option. Printing without sorting.");
                sortOption = 4;
            }

            List<Employee> sortedEmployees = new List<Employee>(employees);
            switch (sortOption)
            {
                case 1:
                    sortedEmployees.Sort((e1, e2) => e1.Name.CompareTo(e2.Name));
                    break;
                case 2:
                    sortedEmployees.Sort((e1, e2) => e1.Department.Name.CompareTo(e2.Department.Name));
                    break;
                case 3:
                    sortedEmployees.Sort((e1, e2) => e1.Position.Title.CompareTo(e2.Position.Title));
                    break;
                case 4:
                default:
                    break; // No sorting
            }

            foreach (var employee in sortedEmployees)
            {
                string departmentName = employee.Department?.Name ?? "No Department";
                string positionTitle = employee.Position?.Title ?? "No Position";

                Console.WriteLine($"ID: {employee.Id}, Name: {employee.Name}, Department: {departmentName}, Position: {positionTitle}");
            }
        }

        public void PrintEmployeeCountByDepartment()
        {
            var countByDepartment = employees.GroupBy(e => e.Department.Name)
                                             .Select(group => new { Department = group.Key, Count = group.Count() });

            foreach (var item in countByDepartment)
            {
                Console.WriteLine($"Department: {item.Department}, Count: {item.Count}");
            }
        }
        public void PrintEmployeesByDepartment()
        {
            if (departments.Count == 0)
            {
                Console.WriteLine("No departments available.");
                return;
            }

            Console.WriteLine("Select Department to list employees:");
            int counter = 1;
            foreach (var dept in Departments)
            {
                Console.WriteLine($"{counter}. {dept.Name}");
                counter++;
            }

            int deptChoice;
            if (!int.TryParse(Console.ReadLine(), out deptChoice) || deptChoice < 1 || deptChoice > departments.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }
            Department selectedDept = departments[deptChoice - 1];

            var employeesInDept = employees.Where(e => e.Department == selectedDept).ToList();

            if (employeesInDept.Count == 0)
            {
                Console.WriteLine($"No employees found in the {selectedDept.Name} department.");
            }
            else
            {
                Console.WriteLine($"Employees in {selectedDept.Name} Department:");
                foreach (var employee in employeesInDept)
                {
                    Console.WriteLine($"ID: {employee.Id}, Name: {employee.Name}");
                }
            }
        }
        public void FindAndPrintEmployeeById(string employeeId)
        {
            var employee = employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee != null)
            {
                string departmentName = employee.Department?.Name ?? "No Department";
                string positionTitle = employee.Position?.Title ?? "No Position";

                Console.WriteLine($"ID: {employee.Id}, Name: {employee.Name}, Date of Birth: {employee.DateOfBirth.ToString("yyyy-MM-dd")}, Department: {departmentName}, Position: {positionTitle}");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }


        public void InitializeSampleData()
        {
            employees.Add(new Employee("E01", "Alice Johnson", new DateTime(1990, 1, 1), departments[0], positions[0]));
            employees.Add(new Employee("E02", "Bob Smith", new DateTime(1985, 5, 15), departments[1], positions[1]));
            employees.Add(new Employee("E03", "Catherine Williams", new DateTime(1993, 8, 23), departments[0], positions[0]));
            employees.Add(new Employee("E04", "David Davis", new DateTime(1989, 12, 7), departments[1], positions[1]));
            employees.Add(new Employee("E05", "Emily Brown", new DateTime(1992, 4, 14), departments[0], positions[0]));
            employees.Add(new Employee("E06", "John Wilson", new DateTime(1991, 7, 31), departments[1], positions[1]));
            employees.Add(new Employee("E07", "Laura Miller", new DateTime(1988, 2, 9), departments[0], positions[0]));
            employees.Add(new Employee("E08", "Michael Lee", new DateTime(1994, 10, 18), departments[1], positions[1]));
            employees.Add(new Employee("E09", "Olivia Anderson", new DateTime(1987, 6, 12), departments[0], positions[0]));
            employees.Add(new Employee("E10", "William Jones", new DateTime(1996, 3, 27), departments[1], positions[1]));
        }
    }
}
