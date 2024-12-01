using CludeTestApi.Data;
using CludeTestApi.Entities;
using CludeTestApi.Exceptions;
using CludeTestApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CludeTestApi.Repositories
{
    public class ProfissionalRepository : IProfissionalRepository
    {

        private readonly DataContext _dataContext;

        public ProfissionalRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        

        public async Task<List<Profissional>> GetProfissionaisAsync()
        {
            try
            {
                return await _dataContext.Profissionais
                    .Include(p => p.Especialidade)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro no acesso ao banco de dados: " + ex.Message);
            }

        }

        public async Task<List<Profissional>> GetProfissionaisByEspecialidadeAsync(int IdEspecialidade)
        {
            try
            {
                return await _dataContext.Profissionais
                    .Include(p => p.Especialidade)
                    .Where(p => p.Especialidade.Id == IdEspecialidade)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro no acesso ao banco de dados: " + ex.Message);
            }

        }

        public async Task<Profissional> GetProfissionalByIdAsync(int Id)
        {
            try
            {
                return await _dataContext.Profissionais
                    .Include(p => p.Especialidade)
                    .SingleOrDefaultAsync(p => p.Id == Id);

            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro no acesso ao banco de dados: " + ex.Message);
            }
        }

        public async Task<int> IncluirProfissionalAsync(Profissional profissional)
        {
            try
            {
                _dataContext.Profissionais.Add(profissional);
                await _dataContext.SaveChangesAsync();

                return profissional.Id;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro no acesso ao banco de dados: " + ex.Message);
            }
        }

        public async Task ExcluirProfissionalAsync(int Id)
        {
            try
            {
                var profissional = await _dataContext.Profissionais.FirstOrDefaultAsync(p => p.Id == Id);

                _dataContext.Remove(profissional);
                await _dataContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro no acesso ao banco de dados: " + ex.Message);
            }
        }

        public async Task EditarProfissionalAsync(Profissional profissional)
        {
            try
            {

                _dataContext.Profissionais.Update(profissional);
                await _dataContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro no acesso ao banco de dados: " + ex.Message);
            }

        }

        public async Task<List<Especialidade>> GetEspecialidadesPorProfissionaisAsync()
        {
            // retorna apenas as especialidades que foram relacionadas a algum profissional cadastrado

            try
            {

                var result = await _dataContext.Profissionais
                 .Include(p => p.Especialidade)
                 .Select(p => p.Especialidade)
                 .Distinct()
                 .ToListAsync();

                return result;

            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro no acesso ao banco de dados: " + ex.Message);
            }


        }

    }
}
