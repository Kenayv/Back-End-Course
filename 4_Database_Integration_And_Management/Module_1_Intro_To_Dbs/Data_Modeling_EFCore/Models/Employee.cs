using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Required]
    [Key]
    public int EmployeeID { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    public DateTime HireDate { get; set; }

    public int DepartmentID { get; set; }

    public Department Department { get; set; }
}
