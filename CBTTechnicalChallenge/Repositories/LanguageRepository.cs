using CBTTechnicalChallenge.Data;
using CBTTechnicalChallenge.Models;

namespace CBTTechnicalChallenge.Repositories
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        public LanguageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
