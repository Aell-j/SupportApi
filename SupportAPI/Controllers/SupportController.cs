using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SupportRequestAPI.Models;
using SupportRequestAPI.Repositories;
using SupportRequestAPI.Services;

namespace SupportRequestAPI.Controllers;

[ApiController]
[Route("api/request")]
public class SupportController : ControllerBase
{
    private readonly ISupportService _supportService;

    public SupportController(ISupportService supportService)
    {
        _supportService = supportService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var requests =  await _supportService.GetAllAsync();
            return Ok(requests);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SupportRequest supportRequest)
    {
        try
        {
            var requests = await _supportService.CreateAsync(supportRequest);
            return StatusCode(201, requests);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("next")]
    public async Task<IActionResult> GetNext()
    {
        try
        {
            var requests = await _supportService.GetFirstAsync();
            return Ok(requests);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPost("process")]
    public async Task<IActionResult> ProcessNext()
    {
        try
        {
            var requests = await _supportService.ProcessFirstAsync();
            return Ok(requests);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCount()
    {
        try
        {
            var requests = await _supportService.GetCountAsync();
            return Ok(requests);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            bool deleted = await _supportService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound("Заявка не найдена");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}