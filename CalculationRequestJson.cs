using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CTAI.trimensons;

public class CalculationRequestJson

{
    private readonly ILogger<CalculationRequest> _logger;

    public CalculationRequestJson(ILogger<CalculationRequest> logger)
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