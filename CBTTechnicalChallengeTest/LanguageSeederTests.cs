using CBTTechnicalChallenge.Data;

namespace CBTTechnicalChallengeTest
{
    public class LanguageSeederTests : IClassFixture<DatabaseFixture>
    {
        private readonly ApplicationDbContext _context;

        public LanguageSeederTests(DatabaseFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void TestLanguageSeeders()
        {
            var languages = _context.Languages.ToList();
            Assert.Equal(2, languages.Count);
            Assert.Contains(languages, l => l.LanguageName == "English");
            Assert.Contains(languages, l => l.LanguageName == "Malay");
        }
    }
}
