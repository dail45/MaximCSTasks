using MaximCSTasks.Models;
using MaximCSTasks.Models.RequestQueries;
using MaximCSTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaximCSTasks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TextController : ControllerBase
{
    private readonly IStringProcessorService _stringProcessorService;
    private readonly SemaphoreSlim _semaphore;

    public TextController(IConfiguration configuration, IStringProcessorService stringProcessorService)
    {
        _stringProcessorService = stringProcessorService;
        _semaphore = new SemaphoreSlim(configuration.GetSection("AppSettings:Settings:IncomingRequestsLimit").Get<int>());
    }

    /// <response code="200">OK</response>
    /// <response code="400">Invalid query params</response>
    /// <response code="500">Unknown error</response>
    /// <response code="503">Service is unavailable</response>
    [HttpGet("process")]
    public IActionResult Get([FromQuery] TextToProcessQueryParams queryParams)
    {
        if (!_semaphore.Wait(0))
        {
            return StatusCode(503, "Service is unavailable");
        }

        try
        {
            var result = _stringProcessorService.ProcessLine(queryParams.Line, queryParams.SorterType);
            if (!string.IsNullOrEmpty(result.Error))
            {
                return BadRequest(new { Data = new { }, Error = result.Error });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Unknown Error");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}