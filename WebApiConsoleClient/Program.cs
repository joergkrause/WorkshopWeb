// See https://aka.ms/new-console-template for more information
using WebApiConsoleClient;

Console.WriteLine("Hello, World!");

var httpClient = new HttpClient();
var client = new DocumentClient(httpClient);
client.BaseUrl = "https://localhost:7242";
var devices = await client.GetAllAsync();

foreach (var device in devices)
{
    Console.WriteLine($"Documents: {device.Name}");
}

Console.WriteLine("Done");
Console.ReadLine();