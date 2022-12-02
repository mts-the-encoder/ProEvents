using Microsoft.AspNetCore.Mvc;
using ProEvents.Application.Contracts;
using ProEvents.Application.Dto;
using ProEvents.Domain;

namespace ProEvents.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _service;

    private Uri uri = new Uri("https://localhost:7242/");
    public EventsController(IEventService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var events = await _service.GetAllEventsAsync(true);

            if (events == null) return NoContent();

            return Ok(events);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status404NotFound,
                $"Error trying to retrieve events. Error: {e.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var res = await _service.GetEventsByIdAsync(id, true);

            if (res == null) return NotFound("No event by Id found");

            return Ok(res);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status404NotFound,
                $"Error trying to retrieve event. Error: {e.Message}");
        }
    }

    [HttpGet("theme/{theme}")]
    public async Task<IActionResult> GetByTheme(string theme)
    {
        try
        {
            var res = await _service.GetAllEventsByThemeAsync(theme, true);

            if (res.Length <= 0) return NoContent();

            return Ok(res);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status404NotFound,
                $"Error trying to retrieve events. Error: {e.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventDto model)
    {
        try
        {
            var res = await _service.AddEvents(model);

            if (res == null) return BadRequest("Error to create event");

            return Created("~api/[controller]", res);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status400BadRequest,
                $"Error trying to create events. Error: {e.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EventDto model)
    {
        try
        {
            var res = await _service.UpdateEvent(id, model);

            if (res == null) return BadRequest("Error to update event");

            return Ok(res);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status404NotFound,
                $"Error trying to update events. Error: {e.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return await _service.DeleteEvent(id) ? Ok("Deleted") : 
                BadRequest("Event not found");
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status404NotFound,
                $"Error trying to delete events. Error: {e.Message}");
        }
    }
}
