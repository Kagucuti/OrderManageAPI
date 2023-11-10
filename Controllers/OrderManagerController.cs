using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.Services;
using OrderManager.Domain.Data_Transfer_Objects;
using OrderManager.Domain.Models;

namespace OrderManageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagerController : ControllerBase
    {
        private readonly IOrderManagerService _service;
        public OrderManagerController(IOrderManagerService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<OrderDTO>> GetAll()
         => await _service.GetAllOrdersAsync();


        [HttpGet("{Id}")]
        public async Task<ActionResult<OrderDTO>> Get(int Id)
            => Ok(await _service.GetSingleOrderAsync(Id));
        [HttpPost]
        public async Task Add([FromBody] OrderDTO orderDTO)
            => await _service.AddOrderAsync(orderDTO);
        [HttpDelete("Id")]
        public async Task Delete(int Id)
            => await _service.DeleteOrderAsync(Id);
        [HttpPut]
        public async Task Uppdate(OrderDTO orderDTO)
            => await _service.UpdateOrderAsync(orderDTO);
        [HttpDelete("orders/{orderId}/orderlines/{orderLineId}")]
        public async Task<IActionResult> RemoveOrderLine(int orderId, int orderLineId)
        {
            try
            {
                await _service.RemoveOrderLineAsync(orderId, orderLineId);

                return Ok("OrderLine removed successfully.");
            }
            catch (Exception ex) {
                return BadRequest($"Error: {ex.Message}");
            }
        }
		[HttpPost("AddOrderLine/{orderId}")]
		
		public async Task AddOrderLine(int orderId, [FromBody]  OrderLineDTO orderLineDTO)
		{
			await _service.AddOrderLineAsync(orderId, orderLineDTO);
		}
	}
}
