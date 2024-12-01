using CludeTestApi.DTOs;

namespace CludeTestApi.Services.Interfaces
{
    public interface IEspecialidadeService
    {
        Task<EspecialidadesResponseDto> GetEspecialidadesAsync();

    }
}
