using CludeTestApi.DTOs;
using CludeTestApi.Entities;
using CludeTestApi.Exceptions;
using CludeTestApi.Repositories.Interfaces;
using CludeTestApi.Services.Interfaces;

namespace CludeTestApi.Services
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IProfissionalRepository _profissionalRepository;
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public ProfissionalService(IProfissionalRepository profissionalRepository, IEspecialidadeRepository especialidadeRepository)
        {
            _profissionalRepository = profissionalRepository;
            _especialidadeRepository = especialidadeRepository;
        }

        public async Task<EspecialidadesResponseDto> GetEspecialidadesPorProfissionaisAsync()
        {

            try
            {
                EspecialidadesResponseDto response; 

                var especialidades = await _profissionalRepository.GetEspecialidadesPorProfissionaisAsync();

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

        public async  Task<ProfissionaisResponseDto> GetProfissionaisAsync()
        {

            try
            {
                var profissionais = await _profissionalRepository.GetProfissionaisAsync();

                var GetProfissionaisResponseDto = new ProfissionaisResponseDto
                {
                    Success = true,
                    Status = 200,
                    Message = "Consulta de profissionais realizada com sucesso.",
                    Profissionais = profissionais.Select(t => new ProfissionalDto
                    {
                        Id = t.Id ,
                        Nome = t.Nome,
                        Especialidade = t.Especialidade.Nome,
                        TipoDocumento = t.Especialidade.TipoDocumento,
                        NumeroDocumento = t.NumeroDocumento,
                        IdEspecialidade = t.Especialidade.Id
                    }).ToList()
                };

                return GetProfissionaisResponseDto;

            }
            catch (Exception ex) 
            {
                throw new ServiceException("Erro na camada de serviço: " + ex.Message);
            }


        }

        public async Task<ProfissionaisResponseDto> GetProfissionaiasByEspecialidadeAsync(int IdEspecialidade)
        {

            try
            {
                ProfissionaisResponseDto responseDto;


                if (!await _especialidadeRepository.VerificaEspecialidadeAsync(IdEspecialidade))
                {
                    responseDto = new ProfissionaisResponseDto
                    {
                        Success = false,
                        Status = 400,
                        Message = "Especialidade não encontrada no banco de dados."                       
                    };

                    return responseDto;
                }

                var profissionais = await _profissionalRepository.GetProfissionaisByEspecialidadeAsync(IdEspecialidade);

                responseDto = new ProfissionaisResponseDto
                {
                    Success = true,
                    Status = 200,
                    Message = "Consulta de profissionais realizada com sucesso.",
                    Profissionais = profissionais.Select(t => new ProfissionalDto
                    {
                        Id = t.Id,
                        Nome = t.Nome,
                        Especialidade = t.Especialidade.Nome,
                        TipoDocumento = t.Especialidade.TipoDocumento,
                        NumeroDocumento = t.NumeroDocumento,
                        IdEspecialidade = t.Especialidade.Id
                    }).ToList()
                };

                return responseDto;

            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro na camada de serviço: " + ex.Message);
            }
        }

        public async Task<ProfissionalResponseDto> GetProfissionalByIdAsync(int id)
        {
            try
            {
                ProfissionalResponseDto responseDto;

                var profissional = await _profissionalRepository.GetProfissionalByIdAsync(id);

                if (profissional == null)
                {
                    responseDto = new ProfissionalResponseDto
                    {
                        Success = false,
                        Status = 400,
                        Message = "Profissional não encontrado no banco de dados para Id= " + id
                    };

                    return responseDto;
                }

                responseDto = new ProfissionalResponseDto
                {
                    Success = true,
                    Status = 200,
                    Message = "Consulta de profissional por Id realizada com sucesso.",
                    Profissional = new ProfissionalDto
                    {
                        Id = profissional.Id,
                        Nome = profissional.Nome,
                        Especialidade = profissional.Especialidade.Nome,
                        TipoDocumento = profissional.Especialidade.TipoDocumento,
                        NumeroDocumento = profissional.NumeroDocumento,
                        IdEspecialidade = profissional.Especialidade.Id
                    }
                };

                return responseDto;

            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro na camada de serviço: " + ex.Message);
            }
        }

        public async Task<ProfissionalResponseDto> IncluirProfissionalAsync(AddProfissionalDto dtoProfissional)
        {
            try { 

                ProfissionalResponseDto responseDto;

                var especialidade = await _especialidadeRepository.GetEspecialidadeAsync(dtoProfissional.IdEspecialidade);

                if (especialidade == null)
                {
                    responseDto = new ProfissionalResponseDto
                    {
                        Success = false,
                        Status = 400,
                        Message = $"Especialidade com id = {dtoProfissional.IdEspecialidade} não encontrada no banco de dados."
                    };

                    return responseDto;
                }

                var profissional = new Profissional
                {
                    Nome = dtoProfissional.Nome,
                    NumeroDocumento = dtoProfissional.NumeroDocumento,
                    Especialidade = especialidade
                };

                int newId = await _profissionalRepository.IncluirProfissionalAsync(profissional);
            
                responseDto = new ProfissionalResponseDto
                {
                    Success = true,
                    Status = 201,
                    Message = $"Profissional incluído no bando de dados com sucesso.",
                    Profissional = new ProfissionalDto
                    {
                        Id = newId,
                        Nome = profissional.Nome,
                        Especialidade = profissional.Especialidade.Nome,
                        TipoDocumento = profissional.Especialidade.TipoDocumento,
                        NumeroDocumento = profissional.NumeroDocumento,
                        IdEspecialidade = profissional.Especialidade.Id
                    }
                };

                return responseDto;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro na camada de serviço: " + ex.Message);
            }
        }

        public async Task<ProfissionalResponseDto> ExcluirProfissionalAsync(int id)
        {
            try
            {
                ProfissionalResponseDto responseDto;

                var profissional = await _profissionalRepository.GetProfissionalByIdAsync(id);

                if (profissional == null)
                {
                    responseDto = new ProfissionalResponseDto
                    {
                        Success = false,
                        Status = 400,
                        Message = "Profissional não encontrado no banco de dados para Id= " + id
                    };

                    return responseDto;
                }

                await _profissionalRepository.ExcluirProfissionalAsync(id);

                responseDto = new ProfissionalResponseDto
                {
                    Success = true,
                    Status = 200,
                    Message = $"Profissional excluído no banco de dados com sucesso."
                   
                };

                return responseDto;


            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro na camada de serviço: " + ex.Message);

            }
        }

        public async Task<ProfissionalResponseDto> EditarProfissionalAsync(EditProfissionalDto dtoProfissional)
        {
            try
            {
                ProfissionalResponseDto responseDto;

                var profissional = await _profissionalRepository.GetProfissionalByIdAsync(dtoProfissional.Id);

                if (profissional == null)
                {
                    responseDto = new ProfissionalResponseDto
                    {
                        Success = false,
                        Status = 400,
                        Message = "Profissional não encontrado no banco de dados para Id= " + dtoProfissional.Id
                    };

                    return responseDto;
                }

                var especialidade = await _especialidadeRepository.GetEspecialidadeAsync(dtoProfissional.IdEspecialidade);

                if (especialidade == null)
                {
                    responseDto = new ProfissionalResponseDto
                    {
                        Success = false,
                        Status = 400,
                        Message = $"Especialidade com id = {dtoProfissional.IdEspecialidade} não encontrada no banco de dados."
                    };

                    return responseDto;
                }

                profissional.Nome = dtoProfissional.Nome;
                profissional.NumeroDocumento = dtoProfissional.NumeroDocumento;
                profissional.Especialidade = especialidade;

                await _profissionalRepository.EditarProfissionalAsync(profissional);

                responseDto = new ProfissionalResponseDto
                {
                    Success = true,
                    Status = 200,
                    Message = "Profissional alterado com sucesso.",
                    Profissional = new ProfissionalDto
                   {
                       Id = profissional.Id,
                       Nome = profissional.Nome,
                       Especialidade = profissional.Especialidade.Nome,
                       TipoDocumento = profissional.Especialidade.TipoDocumento,
                       NumeroDocumento = profissional.NumeroDocumento,
                       IdEspecialidade = profissional.Especialidade.Id
                   }
                };

                return responseDto;


            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro na camada de serviço: " + ex.Message);
            }
        }
    }
}
