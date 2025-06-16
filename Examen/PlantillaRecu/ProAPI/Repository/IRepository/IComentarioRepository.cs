using RestAPI.Models.Entity;

namespace RestAPI.Repository.IRepository
{
    public interface IComentarioRepository : IRepository<ComentarioEntity>
    {
        Task<bool> ExistsAsync(Guid id);
        Task<ComentarioEntity> GetAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
