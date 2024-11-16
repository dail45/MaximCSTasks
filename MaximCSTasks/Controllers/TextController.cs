using MaximCSTasks.Models;
using MaximCSTasks.Models.RequestQueries;
using MaximCSTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaximCSTasks.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TextController: ControllerBase
{
    private readonly StringProcessorService _stringProcessorService;

    public TextController()
    {
        _stringProcessorService = StringProcessorService.Instance;
    }
    
    /// <response code="200">OK</response>
    /// <response code="400">Invalid query params</response>
    /// <response code="500">Unknown error</response>
    [HttpGet("process")]
    public IActionResult Get([FromQuery] TextToProcessQueryParams queryParams)
    {
        try
        {
            var result = _stringProcessorService.ProcessLine(queryParams.Line, queryParams.SorterType);
            if (!string.IsNullOrEmpty(result.Error))
            {
                return BadRequest(new {Data = new {}, Error = result.Error});
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Unknown Error");
        }
    }
}