using System;

class Program
{

    public static async Task DownloadDataAsync()
    {
        try
        {
            await Task.Delay(750);

            int a = 0;
            int b = a/a; // Simulates async task throwing an exception;

            await Task.Delay(750);

            Console.WriteLine("Data downloaded"); 
        } 
        catch (Exception)
        {
            Console.WriteLine("error downloading data1");
        }
        

    }
    public static async Task DownloadDataAsync2()
    {
        await Task.Delay(500);
        Console.WriteLine("Data downloaded2");

    }
    public static async Task DownloadDataAsync3()
    {
        await Task.Delay(2500);
        Console.WriteLine("Data downloaded3");

    }

    static async Task Main(string[] args)
    {
        List<Task> tasks =
        [
            DownloadDataAsync(),
            DownloadDataAsync2(), 
            DownloadDataAsync3() 
        ];
        
        await Task.WhenAll(tasks);
    }
}
