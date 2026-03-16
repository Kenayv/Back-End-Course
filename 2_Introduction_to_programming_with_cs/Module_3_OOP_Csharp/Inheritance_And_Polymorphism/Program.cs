
public interface IAnimal
{
    void Eat();
}

class Animal : IAnimal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("hee hee");
    }

    public void Eat()
    {
        Console.WriteLine("Shee Shee");        
    }
}

class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("woof");
    }

    public void Eat()
    {
        Console.WriteLine("what the dog doing");
    }
}

class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("meow");
    }

     public void Eat()
    {
        Console.WriteLine("eat");
    }
}


class Program
{
    static void Main(string[] args)
    {
        Dog dog = new Dog();
        Cat cat = new Cat();

        dog.MakeSound();
        cat.MakeSound();

        dog.Eat();
        cat.Eat();


        Console.Write("\n\n");

        List<Animal> list =
        [
            new Dog(),
            new Cat(),
            new Dog(),
            new Cat(),
            new Dog(),
            new Cat(),
            new Dog(),
            new Cat(),
        ];

        foreach(var a in list)
        {
            a.MakeSound();
        }
    }
}
