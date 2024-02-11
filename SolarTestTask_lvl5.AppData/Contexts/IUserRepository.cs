using SolarTestTask_lvl5.Domain;

namespace SolarTestTask_lvl5.AppData.Contexts
{
    public interface IUserRepository
    {
        Task<User> FindById(Guid id, CancellationToken cancellation);

        IQueryable<User> GetAll();

        public Task AddAsync(User model, CancellationToken cancellation);

        Task DeleteAsync(User model, CancellationToken cancellation);

        Task EditUserAsync(User edit, CancellationToken cancellation);
    }
}
