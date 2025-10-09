namespace CTAI.trimensons;

public class CalculationResult
{
    public int A { get; }
    public int B { get; }
    public string Operation { get; }
    public object Result { get; }

    public CalculationResult(int a, int b, string operation)
    {
        A = a;
        B = b;
        Operation = operation;

        Result = operation switch
        {
            "add" => a + b,
            "sub" => a - b,
            "mul" => a * b,
            "div" => b != 0 ? (object)(a / b) : "inf",
            _ => "Invalid operation, valid operations are: add, sub, mul, div"
        };
    }
}