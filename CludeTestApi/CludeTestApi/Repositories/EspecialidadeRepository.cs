
using CludeTestApi.Data;
using CludeTestApi.Entities;
using CludeTestApi.Exceptions;
using CludeTestApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CludeTestApi.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {

        private readonly DataContext _dataContext;

        public EspecialidadeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<bool> VerificaEspecialidadeAsync(int especialidadeId)
        {
            try
            {
                var especialidade = await _dataContext.Especialidades.FindAsync(especialidadeId);

                if (especialidade == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro no acesso ao banco de dados: " + ex.Message);
            }
        }

        public async Task<Especialidade> GetEspecialidadeAsync(int especialidadeId)
        {
            try
            {
                return  await _dataContext.Especialidades.FindAsync(especialidadeId);
               
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro no acesso ao banco de dados: " + ex.Message);
            }
        }

        public async Task<List<Especialidade>> GetEspecialidadesAsync()
        {
            try
            {
                return await _dataContext.Especialidades                    
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro no acesso ao banco de dados: " + ex.Message);
            }

        }
    }
}
