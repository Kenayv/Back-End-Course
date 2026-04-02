using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

class User
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    void EncryptUserData()
    {
        if (string.IsNullOrWhiteSpace(Password))
        {
            throw new Exception("Invalid user Password");
        }

        Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(Password));
    }

    public string GenerateHash()
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(ToString()));
            return Convert.ToBase64String(hashBytes);
        }
    }

    public string SerializeUserData()
    {
        if (
            string.IsNullOrWhiteSpace(Name)
            || string.IsNullOrWhiteSpace(Email)
            || string.IsNullOrWhiteSpace(Password)
        )
        {
            Console.WriteLine("Invalid user data - serialization fail");
            return string.Empty;
        }

        EncryptUserData();

        var serializedData = JsonSerializer.Serialize(this);

        return serializedData;
    }

    public static User? deserializeUser(string serializedData, bool isSourceTrusted)
    {
        if (!isSourceTrusted)
        {
            Console.WriteLine("Source is not trusted - Deserialization fail");
            return null;
        }

        //there should be a check to verify if the serializedData is correct format, etc.

        User? user = JsonSerializer.Deserialize<User>(serializedData);

        return user;
    }
}

class Program
{
    public static void Main()
    {
        User user = new User()
        {
            Name = "Tomasz",
            Email = "Tomek.h@gmail.com",
            Password = "has3lko123",
        };

        string serializedData = user.SerializeUserData();

        Console.WriteLine(serializedData);

        User? newUser = User.deserializeUser(serializedData, true);
        User? yetAnotherUser = User.deserializeUser(serializedData, false);

        if (newUser == null)
            Console.WriteLine("serialization 1. failed");

        if (yetAnotherUser == null)
            Console.WriteLine("serialization 2. failed");
    }
}
