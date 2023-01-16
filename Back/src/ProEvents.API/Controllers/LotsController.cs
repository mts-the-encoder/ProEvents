using Microsoft.AspNetCore.Mvc;
using ProEvents.Application.Contracts;
using ProEvents.Application.Dto;

namespace ProEvents.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LotsController : ControllerBase
{
    private readonly ILotService _service;

    public LotsController(ILotService service)
    {
        _service = service;
    }

    [HttpGet("{eventId}")]
    public async Task<IActionResult> Get(int eventId)
    {
        try
        {
            var lots = await _service.GetLotsByEventIdAsync(eventId);

            if (lots is { Length: 0 })
                return NoContent();


            return Ok(lots);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error trying to retrieve lots. Error: {e.Message}");
        }
    }

    [HttpPut("{eventId}")]
    public async Task<IActionResult> Put(int eventId,LotDto[] models)
    {
        try
        {
            var lot = await _service.SaveLot(eventId,models);
            return Ok(lot);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status404NotFound,
                $"Error trying to update lots. Error: {e.Message}");
        }
    }

    [HttpDelete("{eventId}/{lotId}")]
    public async Task<IActionResult> Delete(int eventId,int lotId)
    {
        try
        {
            var lot = await _service.GetLotByIdAsync(eventId,lotId);

            if (lot != null)
                await _service.Delete(lot.EventId,lot.Id);
            return Ok(new
            {
                message = "Lot Deleted"
            });
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status404NotFound,
                $"Error trying to delete lots. Error: {e.Message}");
        }
    }
}