// See https://aka.ms/new-console-template for more information
using WebApiConsoleClient;

Console.WriteLine("Hello, World!");

var httpClient = new HttpClient();
var client = new DevicesClient("https://localhost:7242", httpClient);

var devices = await client.GetAllAsync();

foreach (var device in devices)
{
    Console.WriteLine($"Device: {device.Name}");
}

Console.WriteLine("Done");
Console.ReadLine();