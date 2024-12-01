
using CludeTestApi.Entities;

namespace CludeTestApi.Repositories.Interfaces
{
    public interface IProfissionalRepository
    {

        Task<List<Profissional>> GetProfissionaisAsync();

        Task<List<Profissional>> GetProfissionaisByEspecialidadeAsync(int IdEspecialidade);

        Task<Profissional> GetProfissionalByIdAsync(int Id);

        Task<int> IncluirProfissionalAsync(Profissional profissional);

        Task ExcluirProfissionalAsync(int Id);

        Task EditarProfissionalAsync(Profissional profissional);

        Task<List<Especialidade>> GetEspecialidadesPorProfissionaisAsync();
    }
}
