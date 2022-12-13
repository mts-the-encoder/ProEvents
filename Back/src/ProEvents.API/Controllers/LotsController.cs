using Microsoft.AspNetCore.Mvc;
using ProEvents.Application.Contracts;
using ProEvents.Application.Dto;
using ProEvents.Domain;

namespace ProEvents.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LotsController : ControllerBase
{
    private readonly ILotService _service;

    private Uri _uri = new("https://localhost:7242/");

    public LotsController(ILotService service)
    {
        _service = service;
    }

    [HttpGet("{eventId}")]
    public async Task<IActionResult> Get(int eventId)
    {
        try
        {
            var events = await _service.GetEventByIdAsync(true);

            if (events == null) return NoContent();

            return Ok(events);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error trying to retrieve events. Error: {e.Message}");
        }
    }

    [HttpPut("{eventId}")]
    public async Task<IActionResult> Put(int eventId, LotDto[] models)
    {
        try
        {
            var res = await _service.UpdateEvent(eventId, models);
            if (res == null) return NotFound("No events are found");
            return Ok(res);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status404NotFound,
                $"Error trying to update events. Error: {e.Message}");
        }
    }

    [HttpDelete("{eventId}/{lotId}")]
    public async Task<IActionResult> Delete(int eventId, int lotId)
    {
        try
        {
            var lot = await _service.GetEventByIdAsync(eventId, lotId);
            await _service.DeleteEvent(eventId, lotId);
            return Ok(new { message = "Deleted" });
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status404NotFound,
                $"Error trying to delete events. Error: {e.Message}");
        }
    }
}
