namespace CTAI.trimensons;
public class CoffeeTransaction
{
    public Guid ID { get; set; }

    public DateTime DateTime { get; set; }

    public required string CashType { get; set; }

    public required string Card { get; set; }

    public decimal Money { get; set; }

    public required string CoffeeName { get; set; }
}