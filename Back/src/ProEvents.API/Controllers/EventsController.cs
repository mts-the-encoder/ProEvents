using Microsoft.AspNetCore.Mvc;
using ProEvents.Domain;
using ProEvents.Persistence;

namespace ProEvents.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly ProEventsContext _context;
    public EventsController(ProEventsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Event> Get()
    {
        return _context.Events;
    }
}
