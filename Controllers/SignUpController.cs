using CoffeeShopCore.Services;
using CoffeeShopData.DTOs;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly ISignUpRepository _signupRepo;

        public SignupController(ISignUpRepository signupRepository)
        {
            _signupRepo = signupRepository;
        }


        [HttpPost("user-sign-up")]
        public async Task<IActionResult> Signup([FromBody] SignupDto signupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signupRepo.SignupAsync(signupDto);
            if (result.Succeeded)
            {
                return Ok(ApiResponse.Success("User registered successfully."));
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(ApiResponse.Failed(errors));
            }
        }



    }

}
