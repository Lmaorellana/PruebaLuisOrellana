using AutoMapper;
using DatabaseFirst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoLuisOrellana.Api.Models;
using ProyectoLuisOrellana.Aplication.General;

namespace ProyectoLuisOrellana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IntentosController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IIntentoService intento;

        public IntentosController(IMapper mapper, IIntentoService intento)
        {
            this.mapper = mapper;
            this.intento = intento;
        }

        [HttpPost("RegistrarIntentos")]
        public async Task<ActionResult> RegistrarIntentos(IntentoDto model)
        {

            var _intento = mapper.Map<Intento>(model);

            var token = await intento.IntentoSave(_intento);

            return Ok(token);
        }

        [HttpGet("ObtenerRanking")]
        public async Task<IActionResult> ObtenerRanking(int pageNumber = 1, int pageSize = 10)
        {
            var resultado = await intento.ObtenerRanking(pageNumber, pageSize);
            return Ok(resultado);
        }


        [HttpGet("ObtenerIntentos")]
        public async Task<IActionResult> ObtenerIntentos()
        {
            var resultado = await intento.GetIntentosAll();
            return Ok(resultado);
        }
    }
}
