using Microsoft.AspNetCore.Mvc;
using ProEvents.API.Models;

namespace ProEvents.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    public EventController()
    {
    }

    public IEnumerable<Event> _event = new Event[]
    {
        new Event()
        {
            EventId = 1,
            Theme = ".NET",
            Place = "SP",
            Lot = "1st Lot",
            QtdPeoples = 505,
            EventDate = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
            ImageURL = "photo.png"
        },

        new Event()
        {
            EventId = 2,
            Theme = "Angular",
            Place = "RJ",
            Lot = "2st Lot",
            QtdPeoples = 250,
            EventDate = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"),
            ImageURL = "profile.png"
        }
    };

    [HttpGet]
    public IEnumerable<Event> Get()
    {
        return _event;
    }

    [HttpGet("{id}")]
    public IEnumerable<Event> GetById(int id)
    {
        return _event.Where(x => x.EventId == id);
    }
}
