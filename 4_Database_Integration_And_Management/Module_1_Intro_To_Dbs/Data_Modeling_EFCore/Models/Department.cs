using System.ComponentModel.DataAnnotations;

public class Department
{
    [Required]
    [Key]
    public int DepartmentID { get; set; }

    public string Name { get; set; }

    public List<Employee> Employees { get; set; }
}
