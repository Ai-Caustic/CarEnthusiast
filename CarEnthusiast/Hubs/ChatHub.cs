using Microsoft.AspNetCore.SignalR;
using CarEnthusiast.Models;
using Microsoft.AspNet.SignalR.Hubs;

namespace CarEnthusiast.Hubs
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
       public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

