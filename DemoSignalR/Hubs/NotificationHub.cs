using Microsoft.AspNetCore.SignalR;

namespace DemoSignalR.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task NewMessage(Message msg)
        {
            await Clients.All.SendAsync("MessageReceived", msg);
        }
    }

    public class Message
    {
        public string ClientId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
