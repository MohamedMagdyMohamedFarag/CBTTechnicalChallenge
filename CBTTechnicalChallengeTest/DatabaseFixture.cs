using CBTTechnicalChallenge.Data;
using Microsoft.EntityFrameworkCore;

namespace CBTTechnicalChallengeTest
{
    public class DatabaseFixture : IDisposable
    {
        public ApplicationDbContext Context { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CBTTechnicalChallenge")
                .Options;

            // Assuming configuration is not used in this context, passing null
            Context = new ApplicationDbContext(options, null);
            Context.Database.EnsureCreated();
            VerifyDatabase();
        }

        private void VerifyDatabase()
        {
            // Verify that the existing data is correctly inserted
            var languages = Context.Languages.ToList();
            if (languages == null || !languages.Any())
            {
                throw new Exception("Verification failed: No languages found in the database.");
            }

            //var users = Context.Users.ToList();
            //if (users == null || !users.Any())
            //{
            //    throw new Exception("Verification failed: No users found in the database.");
            //}

            //var otpVerifications = Context.OtpVerifications.ToList();
            //if (otpVerifications == null || !otpVerifications.Any())
            //{
            //    throw new Exception("Verification failed: No OTP verifications found in the database.");
            //}
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
