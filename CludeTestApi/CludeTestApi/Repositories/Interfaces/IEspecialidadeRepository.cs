using CludeTestApi.Entities;

namespace CludeTestApi.Repositories.Interfaces
{
    public interface IEspecialidadeRepository
    {
        Task<bool> VerificaEspecialidadeAsync(int especialidadeId);
        Task<Especialidade> GetEspecialidadeAsync(int especialidadeId);

        Task<List<Especialidade>> GetEspecialidadesAsync();
    }
}
