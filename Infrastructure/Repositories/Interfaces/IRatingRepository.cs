namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

public interface IRatingRepository
{
    Task RateAsync(int userId, string tconst, int value, CancellationToken ct = default);
    Task<IEnumerable<CIT_Portfolio_Project_API.Infrastructure.Persistence.PgSqlFunctionResults.UserRatingHistoryRow>> GetUserRatingHistoryAsync(int userId, CancellationToken ct = default);
}
