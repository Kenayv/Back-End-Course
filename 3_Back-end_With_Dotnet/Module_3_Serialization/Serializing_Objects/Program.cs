using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
Person p = new Person() { Age = 25, Name = "alice" };

using (FileStream fs = new FileStream("person.dat", FileMode.Create))
{
    BinaryWriter bw = new BinaryWriter(fs);
    bw.Write(p.Age);
    bw.Write(p.Name);
}

Console.WriteLine("binary serialization completed");

XmlSerializer xmlSer = new XmlSerializer(typeof(Person));
StringWriter strWriter = new StringWriter();
xmlSer.Serialize(strWriter, p);
File.WriteAllText("person.xml", strWriter.ToString());

Console.WriteLine("xml serialization completed");

File.WriteAllText("person.json", JsonSerializer.Serialize(p));

Console.WriteLine("JSON serialization completed");

app.Run();

public class Person
{
    public required int Age { get; set; }
    public required string Name { get; set; }
}
