using DemoSignalR.Context;
using DemoSignalR.Hubs;
using DemoSignalR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DemoSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PersonsController> _log;

        public PersonsController(IHubContext<NotificationHub> hubContext, ApplicationDbContext context, ILogger<PersonsController> log)
        {
            _hubContext = hubContext;
            _context = context;
            _log = log;
        }

        [HttpGet("Get1")]
        public async Task Get1Async()
        {
            _log.LogInformation(nameof(Get1Async));

            Random rnd = new Random();
            var person = new Person() {
                Name = "Redwan" + rnd.Next(100),
            };
            _context.Persons.Add(person);
            _context.SaveChanges();

            await _hubContext.Clients.All.SendAsync("Get1Client", "success");
        }

        [HttpGet("Get2")]
        public async Task<List<Person>> Get2Async()
        {
            _log.LogInformation(nameof(Get2Async));

            var persons = await _context.Persons.OrderByDescending(x => x.Id).Take(5).ToListAsync();

            return persons;
        }
    }
}
