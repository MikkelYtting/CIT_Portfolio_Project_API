using CIT_Portfolio_Project_API.Infrastructure.Persistence;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Implementations;

public class RatingRepository : IRatingRepository
{
    private readonly AppDbContext _db;
    public RatingRepository(AppDbContext db) => _db = db;

    public async Task RateAsync(int userId, string tconst, int value, CancellationToken ct = default)
    {
        await _db.ExecuteRateAsync(userId, tconst, value, ct);
    }
}
