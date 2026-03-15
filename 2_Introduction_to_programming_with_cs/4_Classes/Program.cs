
class Calculator
{
    public double Add(double a, double b)
    {
        return a + b;
    }
}

class NumberDisplay
{
    public void GisplayNumbers(int[] numbers)
    {
        if(numbers.Length < 10) return;

        for(int i = 0; i<10; i++)
        {
            Console.WriteLine(numbers[i]);
        }
    }
}

class UserInput()
{
    public void GreetUser()
    {
        Console.WriteLine("username: ");
        string username = Console.ReadLine();
        Console.WriteLine("Greetings " + username);
    }
}

class Program
{
    static void Main(string[] args)
    {
       double num1 = 2;
       double num2 = 4;

       Calculator calc = new Calculator();

       Console.WriteLine(calc.Add(num1, num2));

       NumberDisplay nd = new NumberDisplay();

       nd.GisplayNumbers([1,2,3,4,5,5,6,7,8,9,10]);

       UserInput ui = new UserInput();

       ui.GreetUser();
    }
}