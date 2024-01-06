public class ProductionResponse
{
    public List<ResponseProduction> Productions { get; set; }
}

public class ResponseProduction
{
    public string order { get; set; }
    public DateTime date { get; set; }
    public decimal quantity { get; set; }
    public string materialCode { get; set; }
    public decimal cycleTime { get; set; }
}
