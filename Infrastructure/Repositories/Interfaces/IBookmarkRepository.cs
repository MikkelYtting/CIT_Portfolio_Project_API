using CIT_Portfolio_Project_API.Infrastructure.Persistence.PgSqlFunctionResults;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

public interface IBookmarkRepository
{
    Task AddAsync(int userId, string tconst, string? note, CancellationToken ct = default);
    Task<IEnumerable<BookmarkRow>> GetAsync(int userId, CancellationToken ct = default);
    Task DeleteAsync(int userId, string tconst, CancellationToken ct = default);
}
