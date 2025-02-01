namespace CBTTechnicalChallenge.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IOtpVerificationRepository OtpVerifications { get; }
        ILanguageRepository Languages { get; }
        Task<int> CompleteAsync();
    }
}
