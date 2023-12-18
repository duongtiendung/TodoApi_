using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(LoginRequest request)
        {
            try
            {
                var user = await _userService.RegisterUser(request);
                return CreatedAtAction("Register", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Username is Exists.")
                {
                    return BadRequest(ex.Message);
                }
                return Problem(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<LoginResponse>>> Login(LoginRequest request)
        {
            try
            {
                var response = await _userService.LoginUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {

                if (ex.Message == "User not found." || ex.Message == "Wrong password.")
                {
                    return BadRequest(ex.Message);
                }
                else
                {
                    return Problem(ex.Message);
                }
            }
        }
    }
}
