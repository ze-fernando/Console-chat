﻿using Microsoft.AspNetCore.SignalR.Client;

Console.Clear();
string message;
const string url = "http://localhost:5091/chat";

await using var connection = new HubConnectionBuilder().WithUrl(url).Build();

Console.Write("User: ");
var user = Console.ReadLine();

connection.On("Notification", (string name) =>
{
    Console.WriteLine($"{name} Entrou na sala");
});

await connection.StartAsync();

await connection.InvokeCoreAsync("Room", args: new[] {user});

connection.On("Message", async (string name, string msg) =>
{
    Console.WriteLine($"{name}: {msg}");
});

do{
    message = Console.ReadLine();

    if (message != "exit")
    {
        if(message == "clear"){
            Console.Clear();
        }

        connection.InvokeCoreAsync("SendMessage", args: new[] { user, message });
    }

} while(message != "exit");
