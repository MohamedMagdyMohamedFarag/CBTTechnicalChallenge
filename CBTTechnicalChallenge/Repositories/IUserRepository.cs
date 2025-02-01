using CBTTechnicalChallenge.Models;

namespace CBTTechnicalChallenge.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByICNumberAsync(string icNumber);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
