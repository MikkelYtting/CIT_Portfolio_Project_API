using CIT_Portfolio_Project_API.Infrastructure.Persistence;
using CIT_Portfolio_Project_API.Infrastructure.Persistence.PgSqlFunctionResults;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Implementations;

public class BookmarkRepository : IBookmarkRepository
{
    private readonly AppDbContext _db;
    public BookmarkRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(int userId, string tconst, string? note, CancellationToken ct = default)
    {
        await _db.ExecuteAddBookmarkAsync(userId, tconst, note, ct);
    }

    public async Task<IEnumerable<BookmarkRow>> GetAsync(int userId, CancellationToken ct = default)
    {
        return await _db.CallGetBookmarks(userId).ToListAsync(ct);
    }

    public async Task DeleteAsync(int userId, string tconst, CancellationToken ct = default)
    {
        // Assuming a remove function exists. If not, implement via table delete.
        await _db.Database.ExecuteSqlInterpolatedAsync($"select delete_bookmark({userId}, {tconst})", ct);
    }
}
