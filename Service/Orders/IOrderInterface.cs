public interface IOrdersInterface
{
    Task<object> GetOrders();
    Task<object> GetProduction(string email);

    Task<object> SetProduction(
        string email,
        string order,
        string productionDate,
        string productionTime,
        decimal quantity,
        string materialCode,
        decimal cycleTime
    );
}
