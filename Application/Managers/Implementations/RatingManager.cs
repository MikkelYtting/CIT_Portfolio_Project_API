using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

namespace CIT_Portfolio_Project_API.Application.Managers.Implementations;

public class RatingManager : IRatingManager
{
    private readonly IRatingRepository _repo;
    public RatingManager(IRatingRepository repo) { _repo = repo; }

    public async Task RateAsync(int userId, string tconst, int value, CancellationToken ct = default)
    {
        if (value < 1 || value > 10) throw new ArgumentOutOfRangeException(nameof(value), "Rating must be 1-10");
        await _repo.RateAsync(userId, tconst, value, ct);
    }
}
