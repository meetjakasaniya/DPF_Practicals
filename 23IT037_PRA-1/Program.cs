using System;
using System.Collections.Generic;

namespace EmployeeAttendanceApp
{
    class EmployeeAttendance
    {
        private int employeeId;
        private string employeeName;
        private DateTime date;
        private bool isPresent;

        public int EmployeeId
        {
            get { return employeeId; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Employee ID must be positive.");
                employeeId = value;
            }
        }

        public string EmployeeName
        {
            get { return employeeName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Employee name cannot be empty.");
                employeeName = value;
            }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value.Date; }  
        }

        public bool IsPresent
        {
            get { return isPresent; }
            set { isPresent = value; }
        }

        public EmployeeAttendance(int id, string name, DateTime date, bool isPresent)
        {
            EmployeeId = id;
            EmployeeName = name;
            Date = date;
            IsPresent = isPresent;
        }
    }

    class AttendanceManager
    {
        private List<EmployeeAttendance> attendanceRecords = new List<EmployeeAttendance>();

        public void AddRecord(EmployeeAttendance record)
        {
            attendanceRecords.Add(record);
        }

        public void DisplayAllRecords()
        {
            if (attendanceRecords.Count == 0)
            {
                Console.WriteLine("No attendance records found.");
                return;
            }

            Console.WriteLine("\nAll Attendance Records:");
            Console.WriteLine("ID\tName\t\tDate\t\tPresent");
            Console.WriteLine("------------------------------------------------");
            foreach (var record in attendanceRecords)
            {
                Console.WriteLine($"{record.EmployeeId}\t{record.EmployeeName}\t\t{record.Date.ToShortDateString()}\t{(record.IsPresent ? "Yes" : "No")}");
            }
        }

        public int GetTotalPresentDays(int employeeId)
        {
            int count = 0;
            foreach (var record in attendanceRecords)
            {
                if (record.EmployeeId == employeeId && record.IsPresent)
                    count++;
            }
            return count;
        }
    }

    class Program
    {
        static void Main()
        {
            AttendanceManager manager = new AttendanceManager();
            bool exit = false;

            do
            {
                Console.WriteLine("\n===== Employee Attendance Tracker =====");
                Console.WriteLine("1. Add Attendance Record");
                Console.WriteLine("2. View All Attendance Records");
                Console.WriteLine("3. Get Total Present Days by Employee ID");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddAttendanceRecord(manager);
                        break;
                    case "2":
                        manager.DisplayAllRecords();
                        break;
                    case "3":
                        GetTotalPresentDays(manager);
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                        break;
                }

            } while (!exit);

            Console.WriteLine("Thank you for using the Employee Attendance Tracker.");
        }

        static void AddAttendanceRecord(AttendanceManager manager)
        {
            try
            {
                Console.Write("Enter Employee ID (numeric): ");
                if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
                {
                    Console.WriteLine("Invalid Employee ID. Must be a positive number.");
                    return;
                }

                Console.Write("Enter Employee Name: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Employee name cannot be empty.");
                    return;
                }

                Console.Write("Enter Date (MM/dd/yyyy): ");
                string dateInput = Console.ReadLine();
                if (!DateTime.TryParse(dateInput, out DateTime date))
                {
                    Console.WriteLine("Invalid date format.");
                    return;
                }

                Console.Write("Is Present? (Y/N): ");
                string presentInput = Console.ReadLine().Trim().ToUpper();
                bool isPresent;
                if (presentInput == "Y")
                    isPresent = true;
                else if (presentInput == "N")
                    isPresent = false;
                else
                {
                    Console.WriteLine("Invalid input for presence. Please enter Y or N.");
                    return;
                }

                EmployeeAttendance record = new EmployeeAttendance(id, name, date, isPresent);
                manager.AddRecord(record);
                Console.WriteLine("Attendance record added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void GetTotalPresentDays(AttendanceManager manager)
        {
            Console.Write("Enter Employee ID to check total present days: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Invalid Employee ID. Must be a positive number.");
                return;
            }

            int totalPresent = manager.GetTotalPresentDays(id);
            Console.WriteLine($"Employee ID {id} was present on {totalPresent} day(s).");
        }
    }
}