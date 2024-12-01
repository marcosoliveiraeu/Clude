using CludeTestApi.DTOs;
using CludeTestApi.Entities;
using CludeTestApi.Exceptions;
using CludeTestApi.Repositories;
using CludeTestApi.Repositories.Interfaces;
using CludeTestApi.Services.Interfaces;

namespace CludeTestApi.Services
{
    public class EspecialidadeService : IEspecialidadeService
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public EspecialidadeService(IEspecialidadeRepository especialidadeRepository)
        {
            _especialidadeRepository = especialidadeRepository;
        }


        public async Task<EspecialidadesResponseDto> GetEspecialidadesAsync()
        {
            try
            {

                EspecialidadesResponseDto response;

                var especialidades = await _especialidadeRepository.GetEspecialidadesAsync();

                if (especialidades.Count == 0) 
                {
                    response = new EspecialidadesResponseDto
                    {
                        Success = false,
                        Status = 400,
                        Message = "nenhuma especialidade cadastrada no banco de dados."
                    };

                    return response;

                }

                response = new EspecialidadesResponseDto
                {
                    Success = true,
                    Status = 200,
                    Message = "Consulta de especialidades realizada com sucesso.",
                    Especialidades = especialidades.Select(t => new Especialidade
                    {
                        Id = t.Id,
                        Nome = t.Nome,                        
                        TipoDocumento = t.TipoDocumento,
                        
                    }).ToList()
                };

                return response;

            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro na camada de serviço: " + ex.Message);
            }

        }
    }
}
