using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CTAI.trimensons;

public class DemoFunc
{
    private readonly ILogger<DemoFunc> _logger;

    public DemoFunc(ILogger<DemoFunc> logger)
    {
        _logger = logger;
    }

    [Function("CalculationRequest")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "calculator/json")] HttpRequest req)


    {
        var request = await req.ReadFromJsonAsync<CalculationRequest>();
        var calc = new CalculationResult(request.A, request.B, request.Operation);
        _logger.LogInformation("Sum function processed a request.");
        return new OkObjectResult(new Dictionary<string, object>
        {
            { "result", calc.Result },
            { "operator", calc.Operation }
        });
    }
}