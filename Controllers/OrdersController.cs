using Microsoft.AspNetCore.Mvc;

namespace OrdersController {
    [Route("api/[Controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersInterface _orderInterface;

        public OrdersController(IOrdersInterface orderInterface ){
            _orderInterface = orderInterface;
        }
        [HttpGet("GetOrders")]
        public async Task<ActionResult<List<OrderModel>>> GetOrders()
        {
            return Ok(await _orderInterface.GetOrders());
        }
    }
}