using Example01.Binders;
using Example01.Payloads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Internal;

namespace Example01.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    [HttpGet("{id:int}")]
    public IActionResult Get([FromRoute] int id)
    {
        var response = new
        {
            Id = id,
            Source = "FromRoute"
        };
        return Ok(response);
    }
    
    [HttpGet("trace")]
    public IActionResult Get([FromHeader(Name = "X-Trace-Id")] string traceId)
    {
        var response = new
        {
            TraceId = traceId,
            Source = "FromHeader"
        };
        return Ok(response);
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string keyword)
    {
        var response = new
        {
            KeyWord = keyword,
            Source = "FromQuery"
        };
        return Ok(response);
    }
    
    [HttpGet("list")]
    public IActionResult List([FromQuery] ApiQuery query)
    {
        var response = new
        {
            ApiQuery = query,
            Source = "FromQuery"
        };
        return Ok(response);
    }
    
    [HttpGet("date")]
    public IActionResult Date([FromServices] ISystemClock systemClock)
    {
        var response = new
        {
            UtcNow = systemClock.UtcNow,
            Source = "FromServices"
        };
        return Ok(response);
    }
    
    [HttpGet("custom")]
    public IActionResult Custom([FromQuery] [ModelBinder(BinderType = typeof(BooleanModelBinder))] bool answer)
    {
        var response = new
        {
            Answer = answer,
            Source = "CustomBinding"
        };
        return Ok(response);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] ApiRequest request)
    {
        var response = new
        {
            ApiResponse = new ApiResponse($"{request.FirstName} {request.LastName}"),
            Source = "FromBody"
        };
        return Ok(response);
    }
    
    [HttpPut("{id:int}")]
    public IActionResult Put([FromRoute] int id, [FromBody] ApiRequest request)
    {
        return request.Id == id ? NoContent() : BadRequest();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return id > 0 ? NoContent() : BadRequest();
    }
}
