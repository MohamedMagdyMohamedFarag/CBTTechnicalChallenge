using CBTTechnicalChallenge.Data;
using CBTTechnicalChallenge.Models;

namespace CBTTechnicalChallenge.Repositories
{
    public class OtpVerificationRepository : Repository<OtpVerification>, IOtpVerificationRepository
    {
        public OtpVerificationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
