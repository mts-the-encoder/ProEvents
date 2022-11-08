using Microsoft.AspNetCore.Mvc;
using ProEvents.API.Data;
using ProEvents.API.Models;

namespace ProEvents.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly DataContext _context;
    public EventsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Event> Get()
    {
        return _context.Events;
    }

    [HttpGet("{id}")]
    public Event GetById(int id)
    {
        return _context.Events.FirstOrDefault(x => x.EventId == id);
    }

}
