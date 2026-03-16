
using System.Runtime.CompilerServices;

class Person
{
    public string name;
    public int age;

    public void greet()
    {
        Console.WriteLine("hejka " + name);
    }
}

class Program
{
    static void Main(string[] args)
    {
            Person person = new Person();

            person.age = 19;
            person.name = "name";

            person.greet();
    }
}
