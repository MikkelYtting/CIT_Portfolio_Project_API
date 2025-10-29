using CIT_Portfolio_Project_API.Application.DTOs;
using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

namespace CIT_Portfolio_Project_API.Application.Managers.Implementations;

public class AnalyticsManager : IAnalyticsManager
{
    private readonly IAnalyticsRepository _repo;
    public AnalyticsManager(IAnalyticsRepository repo) { _repo = repo; }

    public async Task<IEnumerable<PersonDto>> PopularActorsInMovieAsync(string tconst, CancellationToken ct = default)
    {
        var rows = await _repo.PopularActorsInMovieAsync(tconst, ct);
        return rows.Select(r => new PersonDto { Nconst = r.Nconst, Name = r.Name, Links = new List<LinkDto> { new("self", $"/api/people/{r.Nconst}") } });
    }

    public async Task<IEnumerable<MovieDto>> SimilarMoviesAsync(string tconst, CancellationToken ct = default)
    {
        var rows = await _repo.SimilarMoviesAsync(tconst, ct);
        return rows.Select(r => new MovieDto { Tconst = r.Tconst, Title = r.Title, Links = new List<LinkDto> { new("self", $"/api/movies/{r.Tconst}") } });
    }

    public async Task<IEnumerable<string>> PersonWordsAsync(string name, CancellationToken ct = default)
    {
        var rows = await _repo.PersonWordsAsync(name, ct);
        return rows.Select(r => r.Word);
    }

    public async Task<IEnumerable<PersonDto>> CoPlayersAsync(string actor, CancellationToken ct = default)
    {
        var rows = await _repo.CoPlayersAsync(actor, ct);
        return rows.Select(r => new PersonDto { Nconst = r.Nconst, Name = r.Name, Links = new List<LinkDto> { new("self", $"/api/people/{r.Nconst}") } });
    }

    public async Task<IEnumerable<MovieDto>> ExactMatchAsync(string query, CancellationToken ct = default)
    {
        var rows = await _repo.ExactMatchAsync(query, ct);
        return rows.Select(r => new MovieDto { Tconst = r.Tconst, Title = r.Title, Links = new List<LinkDto> { new("self", $"/api/movies/{r.Tconst}") } });
    }

    public async Task<IEnumerable<MovieDto>> BestMatchAsync(string query, CancellationToken ct = default)
    {
        var rows = await _repo.BestMatchAsync(query, ct);
        return rows.Select(r => new MovieDto { Tconst = r.Tconst, Title = r.Title, Links = new List<LinkDto> { new("self", $"/api/movies/{r.Tconst}") } });
    }
}
