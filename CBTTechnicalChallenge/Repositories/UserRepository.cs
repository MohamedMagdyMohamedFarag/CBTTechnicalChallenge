using CBTTechnicalChallenge.Data;
using CBTTechnicalChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CBTTechnicalChallenge.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByICNumberAsync(string icNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.ICNumber == icNumber);
        }
    }
}
