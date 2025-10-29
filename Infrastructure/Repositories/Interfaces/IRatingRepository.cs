namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

public interface IRatingRepository
{
    Task RateAsync(int userId, string tconst, int value, CancellationToken ct = default);
}
