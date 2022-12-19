using Microsoft.AspNetCore.Mvc;
using ProEvents.Application.Contracts;
using ProEvents.Application.Dto;

namespace ProEvents.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _service;
    private readonly IWebHostEnvironment _environment;

    public EventsController(IEventService service, IWebHostEnvironment environment)
    {
        _service = service;
        _environment = environment;
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
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error trying to retrieve events. Error: {e.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var res = await _service.GetEventByIdAsync(id, true);

            if (res == null) return NotFound("No event was found");

            return Ok(res);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error trying to retrieve event. Error: {e.Message}");
        }
    }

    [HttpGet("theme/{theme}")]
    public async Task<IActionResult> GetByTheme(string theme)
    {
        try
        {
            var res = await _service.GetAllEventsByThemeAsync(theme, true);

            if (res.Length <= 0) return NotFound("Event not found");

            return Ok(res);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error trying to retrieve events. Error: {e.Message}");
        }
    }


    [HttpPost("upload-image/{eventId}")]
    public async Task<IActionResult> UploadImage(int eventId)
    {
        try
        {
            var res = await _service.GetEventByIdAsync(eventId, true);
            if (res == null) return BadRequest("Error to create event");

            var file = Request.Form.Files[0];
            if (file.Length > 0)
            {
                DeleteImage(res.ImageURL);
                res.ImageURL = await SaveImage(file);

            }

            var eventReturn = await _service.UpdateEvent(eventId, res);

            return Ok(eventReturn);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error trying to create events. Error: {e.Message}");
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
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error trying to create events. Error: {e.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EventDto model)
    {
        try
        {
            var res = await _service.UpdateEvent(id, model);
            if (res == null) return NotFound("No events are found");
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
            await _service.DeleteEvent(id);
            return Ok(new { message = "Deleted" });
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status404NotFound,
                $"Error trying to delete events. Error: {e.Message}");
        }
    }

    [NonAction]
    private void DeleteImage(string imageName)
    {
        var imagePath = Path.Combine(_environment.ContentRootPath, @"Resources/Images", imageName);

        if (System.IO.File.Exists(imagePath))
            System.IO.File.Delete(imageName);
    }

    [NonAction]
    private async Task<string> SaveImage(IFormFile imageFile)
    {
        var imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
            .Take(10)
            .ToArray()
        ).Replace(' ', '-');

        imageName = $"{imageName}{DateTime.UtcNow:yymmssfff}{Path.GetExtension(imageFile.FileName)}";

        var imagePath = Path.Combine(_environment.ContentRootPath, @"Resources/Image", imageName);

        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }

        return imageName;
    }
}
