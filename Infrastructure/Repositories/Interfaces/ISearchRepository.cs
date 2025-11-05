using CIT_Portfolio_Project_API.Infrastructure.Persistence.PgSqlFunctionResults;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

public interface ISearchRepository
{
    Task<IEnumerable<SearchRow>> StringSearchAsync(int userId, string text, CancellationToken ct = default);
    Task<IEnumerable<StructuredSearchRow>> StructuredSearchAsync(int userId, string? title, string? plot, string? characters, string? person, CancellationToken ct = default);
    Task<IEnumerable<UserSearchHistoryRow>> GetUserSearchHistoryAsync(int userId, CancellationToken ct = default);
}
