using AutoMapper;
using DatabaseFirst.Models;
using Microsoft.AspNetCore.Mvc;
using ProyectoLuisOrellana.Api.Models;
using ProyectoLuisOrellana.Aplication.General;
using SuperNova.Erp.Base.Domain.Utils;

namespace ProyectoLuisOrellana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;

        public AuthController(IMapper mapper, ITokenService TokenService)
        {
            this.mapper = mapper;
            tokenService = TokenService;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModelDto model)
        {

            var _user = mapper.Map<User>(model);

            var token = await tokenService.GenerateTokenAsync(_user);

            return Ok(token);
        }
    }
}
