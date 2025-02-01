using CBTTechnicalChallenge.Data;

namespace CBTTechnicalChallenge.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            OtpVerifications = new OtpVerificationRepository(_context);
            Languages = new LanguageRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IOtpVerificationRepository OtpVerifications { get; private set; }
        public ILanguageRepository Languages { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
