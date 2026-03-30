using System;
using Microsoft.AspNetCore.Mvc;
using Serilog;

[Route("api/[controller]")]
[ApiController]
public class ErrorHandlingController : ControllerBase
{
    private readonly ILogger<ErrorHandlingController> logger;

    public ErrorHandlingController(ILogger<ErrorHandlingController> logger)
    {
        this.logger = logger;
    }

    [HttpGet("division")]
    public IActionResult GetDivisionResult(int numerator, int denominator)
    {
        try
        {
            var result = numerator / denominator;
            return Ok($"Result : {result}");
        }
        catch (DivideByZeroException e)
        {
            logger.LogError(e, "error, division by zero");
            return BadRequest("Cannot divide by zero");
        }
    }
}
