using BookStore.Models.Dtos;
using BookStore.Service.LoginService;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private ILoginService _loginService;
        public UsersController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("sigin")]
        public async Task<IActionResult> SigIn([FromBody] UserDto userDto)
        {
            if(userDto == null) return BadRequest("Invalid client request");
            var token = await _loginService.ValidateCredentials(userDto);
            if(token == null) return Unauthorized();
            return Ok(token);
        }
        
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            if(tokenDto is null) return BadRequest("Invalid client request");
            var token = await _loginService.ValidateCredentials(tokenDto);
            if(token == null) return BadRequest("Invalid client request");
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var userName = User?.Identity?.Name;
            var result =  await _loginService.RevokeToken(userName);
            if(!result) return BadRequest("Invalid client request");
            return NoContent();
        }
    }
}