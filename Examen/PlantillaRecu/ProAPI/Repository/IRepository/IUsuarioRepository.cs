using RestAPI.Models.DTOs.UserDto;
using RestAPI.Models.Entity;

namespace RestAPI.Repository.IRepository
{
    public interface IUsuarioRepository : IRepository<UsuarioEntity>
    {
        Task<bool> ExistsAsync(int id);
        Task<bool> CreateAsync(UsuarioEntity usuario);
        Task<ICollection<UsuarioEntity>> GetAllAsync();
    }
}
