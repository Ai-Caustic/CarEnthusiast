using Microsoft.AspNetCore.SignalR;
using CarEnthusiast.Models;

namespace CarEnthusiast.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message) => await Clients.All.SendAsync("Received Message", message);
    }
}

