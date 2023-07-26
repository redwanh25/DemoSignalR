using Microsoft.AspNetCore.SignalR;

namespace DemoSignalR.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task NewMessage(string msg)
        {
            await Clients.All.SendAsync("NewMessageReceived", msg);
        }
    }
}
