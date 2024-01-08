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

                var statusValue = (int)statusProperty.GetValue(result);

                return StatusCode(statusValue, result);
            }
            return NoContent();
        }

        [HttpPost("SetProduction")]
        public async Task<ActionResult<List<object>>> SetProduction(
            [FromBody] ProductionRequestModel request
        )
        {
            var result = await _orderInterface.SetProduction(
                request.Email,
                request.Order,
                request.ProductionDate,
                request.ProductionTime,
                request.Quantity,
                request.MaterialCode,
                request.CycleTime
            );
            if (result != null)
            {
                var statusProperty = result.GetType().GetProperty("status");

                var statusValue = (int)statusProperty.GetValue(result);

                return StatusCode(statusValue, result);
            }
            return NoContent();
        }
    }
}
