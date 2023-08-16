using CoffeeShopAPI.Models;
using CoffeeShopCore.Services;
using CoffeeShopData.DTOs;
using CoffeeShopData.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderRepository _orderRepo;
        private readonly UserManager<AppUser> _userManager;
       

        public OrderController(IOrderRepository orderRepo, UserManager<AppUser> userManager )
        {
            _orderRepo = orderRepo;
            _userManager = userManager;
           
        }

        //[HttpPost("place-order")]
        //public async Task<IActionResult> PlaceOrder([FromBody] OrderDto orderDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ApiResponse.Failed(ModelState, "Invalid input."));
        //    }
        //    try
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        if (user == null)
        //        {
        //            return Unauthorized(ApiResponse.Failed("Please log in to continue."));
        //        }                           

        //        await _orderRepo.PlaceOrder(orderDto, User);

        //        return Ok(ApiResponse.Success("Your order was placed successfully."));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ApiResponse.Failed($"An error occurred while processing your order.{ex}"));
        //    }
        //}



        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder_Test([FromBody] OrderDto orderDto, string userId) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Failed(ModelState, "Invalid input."));
            }
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound(ApiResponse.Failed("User not found"));
                }
                await _orderRepo.PlaceOrder_Test(orderDto,userId);
                return Ok(ApiResponse.Success("Your order was placed successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Failed($"An error occurred while processing your order.{ex}"));
            }
        }




        [HttpGet("order-history")]
        public async Task<IActionResult> GetOrderHistoryForCurrentDate()
        {
            try
            {
                DateTime currentDate = DateTime.UtcNow.Date;

                List<Order> orderHistory = await _orderRepo.GetOrderHistoryForDateWithUserDetails(currentDate);
                if (orderHistory == null)
                {
                    return Ok(ApiResponse.Success("No orders found for this particular day"));
                }

                return Ok(ApiResponse.Success(orderHistory));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Failed($"Failed to fetch Order History. {ex}"));
            }
        }





    }
}
