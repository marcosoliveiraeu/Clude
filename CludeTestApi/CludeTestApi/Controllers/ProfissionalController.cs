using CludeTestApi.DTOs;
using CludeTestApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CludeTestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfissionalController : Controller
    {

        private readonly IProfissionalService _profissionalService;

        public ProfissionalController(IProfissionalService profissionalService)
        {
            _profissionalService = profissionalService;            
        }


        /// <summary>
        ///     Consulta de Profissionais
        /// </summary>
        /// <remarks>
        ///     Retorna os profissionais cadastrados no banco de dados
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Lista de profissionais</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(ProfissionaisResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("getProfissionais")]
        public async Task<ActionResult<ProfissionaisResponseDto>> GetProfissionais()
        {
            var response = await _profissionalService.GetProfissionaisAsync();

            if (response.Success == true)       
                return Ok(response);

            return StatusCode(StatusCodes.Status500InternalServerError, response);

        }


        /// <summary>
        ///     Consulta de Profissionais filtrando por especialidade
        /// </summary>
        /// <remarks>
        ///     Retorna os profissionais cadastrados no banco de dados de acordo com a especialidade passada via parametro
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Lista de profissionais</response>
        /// <response code="400">Parâmetros inválidos</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(ProfissionaisResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProfissionaisResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("getProfissionaisByEspecialidade")]
        public async Task<ActionResult<ProfissionaisResponseDto>> GetProfissionaisByEspecialidade([FromQuery] int especialidadeId)
        {
            ProfissionaisResponseDto response = new ProfissionaisResponseDto();
            
            response = await _profissionalService.GetProfissionaiasByEspecialidadeAsync(especialidadeId);

            if (response.Success == true)
                return Ok(response);

            if (response.Status == 400)
                return BadRequest(response);

            return StatusCode(StatusCodes.Status500InternalServerError, response);

        }


        /// <summary>
        ///     Consulta de Profissionais filtrando por Id do profissional
        /// </summary>
        /// <remarks>
        ///     Retorna um profissional de acordo com o Id passado como parametro
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Detalhes do profissional procurado e retornado com sucesso</response>
        /// <response code="400">Parâmetros inválidos</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(ProfissionalResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProfissionalResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("getProfissionalById")]
        public async Task<ActionResult<ProfissionalResponseDto>> GetProfissionalById([FromQuery] int Id)
        {
            ProfissionalResponseDto response;

            response = await _profissionalService.GetProfissionalByIdAsync(Id);

            if (response.Success == true)
                return Ok(response);

            if (response.Status == 400)
                return BadRequest(response);

            return StatusCode(StatusCodes.Status500InternalServerError, response);

        }


        /// <summary>
        ///     Incluir Profissional 
        /// </summary>
        /// <remarks>
        ///     Inclui um profissional no banco de dados de acordo com os parametros passados
        /// </remarks>
        /// <returns></returns>
        /// <response code="201">Profissional incluído com sucesso</response>
        /// <response code="400">Parâmetros inválidos</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(ProfissionalResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProfissionalResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("IncluirProfissional")]
        public async Task<ActionResult<ProfissionalResponseDto>> IncluirProfissional([FromQuery] AddProfissionalDto profissionalDto)
        {
            ProfissionalResponseDto response;

            response = await _profissionalService.IncluirProfissionalAsync(profissionalDto);

            if (response.Success == true)
                return CreatedAtAction(nameof(GetProfissionalById), new { id = response.Profissional.Id }, response);

            if (response.Status == 400)
                return BadRequest(response);

            return StatusCode(StatusCodes.Status500InternalServerError, response);

        }

        /// <summary>
        ///     Excluir profissional
        /// </summary>
        /// <remarks>
        ///     Exclui um profissional de acordo com o Id passado como parametro
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Profissional excluído com sucesso</response>
        /// <response code="400">Parâmetros inválidos</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(ProfissionalResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProfissionalResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("ExcluirProfissional")]
        public async Task<ActionResult<ProfissionalResponseDto>> ExcluirProfissional([FromQuery] int Id)
        {
            ProfissionalResponseDto response;

            response = await _profissionalService.ExcluirProfissionalAsync(Id);

            if (response.Success == true)
                return Ok(response);

            if (response.Status == 400)
                return BadRequest(response);

            return StatusCode(StatusCodes.Status500InternalServerError, response);

        }



        /// <summary>
        ///     Editar Profissional 
        /// </summary>
        /// <remarks>
        ///     Altera os dados do profissional no banco de dados de acordo com os parametros passados
        /// </remarks>
        /// <returns></returns>
        /// <response code="201">Profissional alterado com sucesso</response>
        /// <response code="400">Parâmetros inválidos</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(ProfissionalResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProfissionalResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("EditarProfissional")]
        public async Task<ActionResult<ProfissionalResponseDto>> EditarProfissional([FromQuery] EditProfissionalDto profissionalDto)
        {
            ProfissionalResponseDto response;

            response = await _profissionalService.EditarProfissionalAsync(profissionalDto);

            if (response.Success == true)
                return Ok(response);

            if (response.Status == 400)
                return BadRequest(response);

            return StatusCode(StatusCodes.Status500InternalServerError, response);

        }

    }
}
