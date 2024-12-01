using CludeTestApi.DTOs;
using CludeTestApi.Services;
using CludeTestApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CludeTestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadeController : Controller
    {
        private readonly IEspecialidadeService _especialidadeService;

        public EspecialidadeController(IEspecialidadeService especialidadeService)
        {
            _especialidadeService = especialidadeService;
        }

        /// <summary>
        ///     Consulta de Especialidades
        /// </summary>
        /// <remarks>
        ///     Retorna todas as especialidades cadastradas no banco de dados
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Lista de especialidades</response>
        /// <response code="400">Nenhuma especialidade cadastrada no banco de dados</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(EspecialidadesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EspecialidadesResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("getEspecialidades")]
        public async Task<ActionResult<EspecialidadesResponseDto>> GetEspecialidades()
        {
            var response = await _especialidadeService.GetEspecialidadesAsync();

            if (response.Success == true)
                return Ok(response);

            if(response.Status == 400)
                return BadRequest(response);

            return StatusCode(StatusCodes.Status500InternalServerError, response);

        }



    }
}
