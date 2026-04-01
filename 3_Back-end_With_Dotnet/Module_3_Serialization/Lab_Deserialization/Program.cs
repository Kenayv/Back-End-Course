using System.Text.Json;
using System.Xml.Serialization;

public class Person
{
    public required string Name { get; set; }
    public required int Age { get; set; }
}

class Program
{
    static async Task Main()
    {
        Console.WriteLine("\nBinary serialization-deserialization started");

        using (FileStream fs = new FileStream("person.dat", FileMode.Create))
        using (BinaryWriter bw = new BinaryWriter(fs))
        {
            Person person = new Person() { Name = "Alice", Age = 25 };
            bw.Write(person.Age);
            bw.Write(person.Name);
        }

        using (FileStream fsRead = new FileStream("person.dat", FileMode.Open))
        using (BinaryReader br = new BinaryReader(fsRead))
        {
            int age = br.ReadInt32();
            string name = br.ReadString();

            Person deserializedP = new Person() { Name = name, Age = age };

            Console.WriteLine($"Name: {deserializedP.Name}, Age: {deserializedP.Age}");
        }

        Console.WriteLine("Binary serialization-deserialization completed\n");

        Console.WriteLine("XML serialization-deserialization started");

        Person examplePerson = new Person() { Name = "skibidi sigma", Age = 67 };

        XmlSerializer serializer = new XmlSerializer(typeof(Person));
        StringWriter sw = new StringWriter();
        serializer.Serialize(sw, examplePerson);
        StringReader sr = new StringReader(sw.ToString());

        Person deserialized = (Person)serializer.Deserialize(sr);
        Console.WriteLine($"Name: {deserialized.Name}, Age: {deserialized.Age}");

        Console.WriteLine(" XML serialization-deserialization completed\n");

        Console.WriteLine("JSON serialization-deserialization started");

        Person yetAnotherPerson = new Person() { Age = 22, Name = "44" };
        string JsonPerson = JsonSerializer.Serialize(yetAnotherPerson);
        Person deserializedJsonP = JsonSerializer.Deserialize<Person>(JsonPerson);

        Console.WriteLine($"Name: {deserializedJsonP.Name}, Age: {deserializedJsonP.Age}");

        Console.WriteLine(" JSON serialization-deserialization completed\n");
    }
}
