
using System.ComponentModel.DataAnnotations;

public class Product
{
    [Required]
    [Key]
    public int ProductId {get;set;}

    public string Name {get;set;}

    public double Price {get;set;}
}