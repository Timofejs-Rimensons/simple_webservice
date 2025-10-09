using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CTAI.trimensons;

public class CalculatorTrigger
{
    private readonly ILogger<CalculatorTrigger> _logger;

    public CalculatorTrigger(ILogger<CalculatorTrigger> logger)
    {
        _logger = logger;
    }

    [Function("CalculatorTrigger")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculator/{a:int}/{operation}/{b:int}")] HttpRequest req,
        int a,
        int b,
        string operation)
    {
        var calc = new CalculationResult(a, b, operation);
        _logger.LogInformation("Sum function processed a request.");
        return new OkObjectResult(new Dictionary<string, object>
        {
            { "result", calc.Result },
            { "operator", operation }
        });
    }
}
