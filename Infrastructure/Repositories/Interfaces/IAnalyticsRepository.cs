using CIT_Portfolio_Project_API.Infrastructure.Persistence.PgSqlFunctionResults;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

public interface IAnalyticsRepository
{
    Task<IEnumerable<PopularActorRow>> PopularActorsInMovieAsync(string tconst, CancellationToken ct = default);
    Task<IEnumerable<SimilarTitleRow>> SimilarMoviesAsync(string tconst, CancellationToken ct = default);
    Task<IEnumerable<PersonWordRow>> PersonWordsAsync(string name, CancellationToken ct = default);
    Task<IEnumerable<CoPlayerRow>> CoPlayersAsync(string actor, CancellationToken ct = default);
    Task<IEnumerable<ExactMatchRow>> ExactMatchAsync(string query, CancellationToken ct = default);
    Task<IEnumerable<BestMatchRow>> BestMatchAsync(string query, CancellationToken ct = default);
}
