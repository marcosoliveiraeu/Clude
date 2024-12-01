using CludeTestApi.DTOs;

namespace CludeTestApi.Services.Interfaces
{
    public interface IProfissionalService
    {

        Task<ProfissionaisResponseDto> GetProfissionaisAsync();

        Task<ProfissionaisResponseDto> GetProfissionaiasByEspecialidadeAsync(int IdEspecialidade);

        Task<ProfissionalResponseDto> GetProfissionalByIdAsync(int Id);

        Task<ProfissionalResponseDto> IncluirProfissionalAsync(AddProfissionalDto profissional);

        Task<ProfissionalResponseDto> ExcluirProfissionalAsync(int Id);

        Task<ProfissionalResponseDto> EditarProfissionalAsync(EditProfissionalDto profissional);

        Task<EspecialidadesResponseDto> GetEspecialidadesPorProfissionaisAsync();

    }
}
