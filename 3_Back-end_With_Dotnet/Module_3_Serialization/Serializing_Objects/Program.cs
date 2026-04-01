using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

void RunSerializationExample()
{
    Person p = new Person() { Age = 25, Name = "alice" };

    FileStream fs = new FileStream("person.dat", FileMode.Create);
    BinaryWriter bw = new BinaryWriter(fs);
    bw.Write(p.Age);
    bw.Write(p.Name);

    Console.WriteLine("binary serialization completed");

    XmlSerializer xmlSer = new XmlSerializer(typeof(Person));

    StringWriter strWriter = new StringWriter();
    xmlSer.Serialize(strWriter, p);
    File.WriteAllText("person.xml", strWriter.ToString());

    Console.WriteLine("xml serialization completed");

    File.WriteAllText("person.json", JsonSerializer.Serialize(p));

    Console.WriteLine("JSON serialization completed");
}

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost(
    "/json",
    async (HttpContext context) =>
    {
        var person = await context.Request.ReadFromJsonAsync<Person>();
        return TypedResults.Json(person);
    }
);

app.MapPost(
    "/xml",
    async (HttpContext context) =>
    {
        var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        var XmlSerializer = new XmlSerializer(typeof(Person));
        var stringReader = new StringReader(body);
        var person = XmlSerializer.Deserialize(stringReader);

        return TypedResults.Ok(person);
    }
);

app.Run();

RunSerializationExample();

public class Person
{
    public required int Age { get; set; }
    public required string Name { get; set; }
}
