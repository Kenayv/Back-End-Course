using System;


/*
Simple console application that allows the user to manage an inventory of products.

This is a submission project for the course "Foundations of Coding" on the Codecademy platform. 

Constraints and grading criteria:

* There are a total of 25 points for this project.
* (5pts) Did you write project requirements and objectives?
* (5pts) Did you include a design outline for your project?
* (5pts) Does your project code include at least one control structure like if-else and switch statements?
* (5pts) Does your project code include at least one loop, like for and while? 
* (5pts) Does your project code define and call a method?  

Course certificate link: https://coursera.org/share/cb3020927206d1ad88656c1560be4efd
*/



//Simple class to contain product info and allow easier displaying in console
class Product
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    public Product(string name, int quantity, double price)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
    }

    public override string ToString()
    {
        return $"Name: {Name} Quantity: {Quantity} price: {Price}";
    }
}


class InventoryManagement
{
    //Class to manage the inventory of products. It contains a list of products and methods to manipulate it.

    //THE CLASS ASSUMES THAT INPUT IS ALWAYS CORRECT AMD DOES NOT HAVE ANY ERROR HANDLING. THIS IS DONE TO KEEP THE CODE SIMPLE AND FOCUSED ON THE TOPIC OF THE COURSE.
    List<Product> Products;

    public InventoryManagement()
    {
        Products = new List<Product>();
    }

    public int ProductCount ()
    {
        return Products.Count;
    }
    public void AddProduct(string name, int quantity, double price) 
    {
        Product p = new Product(name, quantity, price);

        Products.Add(p);
    }

    public void UpdateStock(string name, int newQuantity) 
    {
        foreach (Product p in Products)
        {
            if (p.Name == name)
            {
                p.Quantity = newQuantity;
                return;
            }
        }
    }

    public void RemoveProduct(string name) 
    {
        for (int i = 0; i < Products.Count; i++)
        {
            if (Products[i].Name == name)
            {
                Products.RemoveAt(i);
                return;
            }
        }
    }

    public void PrintProductList() 
    {

        if (Products.Count == 0)
        {
            Console.WriteLine("Inventory is empty");
            return;
        }

        foreach (Product p in Products)
        {
            Console.WriteLine(p.ToString());
        }
    }

    public bool ProductExists(string name) 
    {
        foreach(Product p in Products)
        {
            if (p.Name == name) return true;
        }

        return false;
    }
}

class Program
{
    public static void DisplayUserMenu()
    {
        Console.WriteLine("\n\nInventory Management System\n");

        Console.WriteLine("1: Add Product to the system");
        Console.WriteLine("2. Update Product's stock level");
        Console.WriteLine("3. Remove Product from the system");
        Console.WriteLine("4. Display information about all the products");
        Console.WriteLine("0. Exit the system\n\n");
    }
    public static void AddProductAction(InventoryManagement inventory)
    {
        Console.WriteLine("Product name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Product Quantity: ");
        int quantity = int.Parse(Console.ReadLine());

        Console.WriteLine("Product price: ");
        double price = double.Parse(Console.ReadLine());

        if (inventory.ProductExists(name) || quantity < 0 || price < 0)
        {
            Console.WriteLine("\nCould not add product to the list.");
        }
        else
        {
            inventory.AddProduct(name, quantity, price);
            Console.WriteLine($"\nProduct: {name} was added to the list");
        }
    }

    public static void UpdateStockAction(InventoryManagement inventory)
    {
        Console.WriteLine("Product name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Product Quantity: ");
        int quantity = int.Parse(Console.ReadLine());

        if (!inventory.ProductExists(name) || quantity < 0)
        {
            Console.WriteLine("\nCould not change product's quantity.");
        }
        else
        {
            inventory.UpdateStock(name, quantity);
            Console.WriteLine($"\nchanged Products: {name} quantity to: {quantity}");
        }
    }

    public static void RemoveProductAction(InventoryManagement inventory)
    {
        Console.WriteLine("Product name: ");
        string name = Console.ReadLine();

        if (!inventory.ProductExists(name))
        {
            Console.WriteLine("\nCould not find the product.");
            return;
        }

        Console.WriteLine($"\nare you sure you want to delete product {name}? (Y/N):");

        string decision = Console.ReadLine().ToLower();

        if(decision == "y")
        {
            inventory.RemoveProduct(name);
            Console.WriteLine("\nSuccesfully removed the product.");
        }
        else
        {
            Console.WriteLine("\nDid not remove the product.");
        }

    }

    public static void ViewProductsAction(InventoryManagement inventory)
    {
        Console.WriteLine($"\nProduct count: {inventory.ProductCount()}\n");
        inventory.PrintProductList();
    }

    public static void ExitProgramAction() => Console.WriteLine("\nExiting the program...");    
    public static void UnknownKeyAction() => Console.WriteLine("\nUnknown action key");
    


    static void Main(string[] args)
    {
        InventoryManagement inventory = new InventoryManagement();

        while(true)
        {
            DisplayUserMenu();
            string actionKey = Console.ReadLine();
            switch (actionKey)
            {
                case "1":
                    AddProductAction(inventory);
                    break;

                case "2":
                    UpdateStockAction(inventory);
                    break;

                case "3":
                    RemoveProductAction(inventory);
                    break;

                case "4":
                    ViewProductsAction(inventory);
                    break;

                case "0":
                    ExitProgramAction();
                    return;

                default:
                    UnknownKeyAction();
                    break;
            }
        }
    }
}