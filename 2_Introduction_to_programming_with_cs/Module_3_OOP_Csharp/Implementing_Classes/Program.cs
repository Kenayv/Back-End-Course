
using System.Runtime.CompilerServices;

class Person
{
    public string Name;
    public int Age;

    public void Greet()
    {
        Console.WriteLine("hejka " + Name);
    }
}

class Program
{
    static void Main(string[] args)
    {
            Person person = new Person();

            person.Age = 19;
            person.Name = "name";

            person.Greet();
    }
}
