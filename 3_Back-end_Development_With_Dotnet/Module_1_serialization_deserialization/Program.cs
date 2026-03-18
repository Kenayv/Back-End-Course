using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;
using Newtonsoft.Json;
public class Person
{
    public string Name {get;set;}
    public int Age {get;set;}
}

class Program
{
    static void Main(string[] args)
    {
        string json = "{\"Name\": \"Juan Pablo\", \"Age\": 20}";

        Person person = JsonConvert.DeserializeObject<Person>(json);
        Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");

        Person newPerson = new Person() {Name = "serialization", Age=13};

        string newPersonJson = JsonConvert.SerializeObject(newPerson);

        Console.WriteLine(newPersonJson);
    }
}