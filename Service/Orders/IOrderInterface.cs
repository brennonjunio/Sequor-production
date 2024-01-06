public interface IOrdersInterface
{
       Task<ApiResponse<List<OrderModel>>> GetOrders();
}