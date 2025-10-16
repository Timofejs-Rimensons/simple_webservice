using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CTAI.trimensons;

public class Transactions
{
    private readonly ILogger<Transactions> _logger;

    public Transactions(ILogger<Transactions> logger)
    {
        _logger = logger;
    }

    [Function("Transactions")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route="transactions")] HttpRequest req)
    {
        try
        {
            TransactionRepository repository = new TransactionRepository();
            List<CoffeeTransaction> transactions = await repository.GetAllTransactionsAsync();
            return new OkObjectResult(transactions);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"An error occured while fetching transactions: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}