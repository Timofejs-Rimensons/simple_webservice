using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CTAI.trimensons;

public class TransactionRepository
{
    private readonly string _connectionString;

    public TransactionRepository()
    {
        _connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
    }

    public async Task<List<CoffeeTransaction>> GetAllTransactionsAsync()
    {
        List<CoffeeTransaction> transactions = new List<CoffeeTransaction>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT ID, DateTime, CashType, Card, Money, CoffeeName FROM Transactions";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CoffeeTransaction transaction = new CoffeeTransaction
                            {
                                ID = reader.GetGuid(reader.GetOrdinal("ID")),
                                DateTime = reader.GetDateTime(reader.GetOrdinal("DateTime")),
                                CashType = reader["CashType"] as string,
                                Card = reader["Card"] as string,
                                Money = reader.GetDecimal(reader.GetOrdinal("Money")),
                                CoffeeName = reader["CoffeeName"] as string
                            };
                            transactions.Add(transaction);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving transactions: {ex.Message}", ex);
                }
            }
        }
        return transactions;
    }
}