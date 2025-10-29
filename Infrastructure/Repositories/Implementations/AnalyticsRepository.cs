using CIT_Portfolio_Project_API.Infrastructure.Persistence;
using CIT_Portfolio_Project_API.Infrastructure.Persistence.PgSqlFunctionResults;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Implementations;

public class AnalyticsRepository : IAnalyticsRepository
{
    private readonly AppDbContext _db;
    public AnalyticsRepository(AppDbContext db) => _db = db;

    public Task<IEnumerable<PopularActorRow>> PopularActorsInMovieAsync(string tconst, CancellationToken ct = default)
        => _db.CallPopularActorsInMovie(tconst).ToListAsync(ct).ContinueWith(t => (IEnumerable<PopularActorRow>)t.Result, ct);

    public Task<IEnumerable<SimilarTitleRow>> SimilarMoviesAsync(string tconst, CancellationToken ct = default)
        => _db.CallSimilarMovies(tconst).ToListAsync(ct).ContinueWith(t => (IEnumerable<SimilarTitleRow>)t.Result, ct);

    public Task<IEnumerable<PersonWordRow>> PersonWordsAsync(string name, CancellationToken ct = default)
        => _db.CallPersonWords(name).ToListAsync(ct).ContinueWith(t => (IEnumerable<PersonWordRow>)t.Result, ct);

    public Task<IEnumerable<CoPlayerRow>> CoPlayersAsync(string actor, CancellationToken ct = default)
        => _db.CallCoPlayers(actor).ToListAsync(ct).ContinueWith(t => (IEnumerable<CoPlayerRow>)t.Result, ct);

    public Task<IEnumerable<ExactMatchRow>> ExactMatchAsync(string query, CancellationToken ct = default)
        => _db.CallExactMatch(query).ToListAsync(ct).ContinueWith(t => (IEnumerable<ExactMatchRow>)t.Result, ct);

    public Task<IEnumerable<BestMatchRow>> BestMatchAsync(string query, CancellationToken ct = default)
        => _db.CallBestMatch(query).ToListAsync(ct).ContinueWith(t => (IEnumerable<BestMatchRow>)t.Result, ct);
}
