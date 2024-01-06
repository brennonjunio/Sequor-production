using Microsoft.AspNetCore.Mvc;

namespace OrdersController
{
    [Route("api/[Controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersInterface _orderInterface;

        public OrdersController(IOrdersInterface orderInterface)
        {
            _orderInterface = orderInterface;
        }

        [HttpGet("GetOrders")]
        public async Task<ActionResult<List<OrderModel>>> GetOrders()
        {
            return Ok(await _orderInterface.GetOrders());
        }

        [HttpGet("GetProduction")]
        public async Task<ActionResult<List<ProductionModel>>> GetProduction(
            [FromQuery] string email
        )
        {
            var result = await _orderInterface.GetProduction(email);

            if (result != null)
            {
                var statusProperty = result.GetType().GetProperty("status");

                if (statusProperty != null)
                {
                    var statusValue = (int)statusProperty.GetValue(result);

                    return StatusCode(statusValue, result);
                }
            }
            var description = "Erro não reconhecido";
            return BadRequest(description);
        }
    }
}
