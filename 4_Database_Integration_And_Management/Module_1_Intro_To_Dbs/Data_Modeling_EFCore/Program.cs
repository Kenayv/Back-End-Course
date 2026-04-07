var context = new HrDbContext();

// context.Departments.Add(
//     new Department
//     {
//         Name = "Engineering",
//         Employees = new List<Employee>
//         {
//             new Employee
//             {
//                 FirstName = "Ryan",
//                 LastName = "Gosling",
//                 HireDate = DateTime.Now,
//             },
//             new Employee
//             {
//                 FirstName = "Ryan",
//                 LastName = "Ford",
//                 HireDate = DateTime.Now,
//             },
//         },
//     }
// );

context.SaveChanges();

var employees = context.Employees.Where(e => e.Department.Name == "Engineering").ToList();

Console.WriteLine($"Employees in engineering: {employees.Count}");

foreach (var e in employees)
{
    Console.WriteLine($"{e.EmployeeID}  {e.FirstName}  {e.LastName}");
}
