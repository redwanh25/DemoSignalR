using DemoSignalR.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DemoSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly List<string> list = new() { "Rahim", "Karim" };
        private readonly IHubContext<NotificationHub> hubContext;

        public PersonsController(IHubContext<NotificationHub> context)
        {
            hubContext = context;
        }

        [HttpGet("Get1")]
        public async Task Get1Async()
        {
            await hubContext.Clients.All.SendAsync("MessageReceived2", "success");
        }

        [HttpGet("Get2")]
        public List<string> Get2()
        {
            return list;
        }
    }
}
