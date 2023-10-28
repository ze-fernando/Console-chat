using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<MyHub>("/chat");

app.Run();

class MyHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("Message", user, message);
    }

    public async Task Room(string user)
    {
        await Clients.All.SendAsync("Notification", user);
    }
}