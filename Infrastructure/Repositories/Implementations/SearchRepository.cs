using CIT_Portfolio_Project_API.Infrastructure.Persistence;
using CIT_Portfolio_Project_API.Infrastructure.Persistence.PgSqlFunctionResults;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Implementations;

public class SearchRepository : ISearchRepository
{
    private readonly AppDbContext _db;
    public SearchRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<SearchRow>> StringSearchAsync(int userId, string text, CancellationToken ct = default)
        => await _db.CallStringSearch(userId, text).ToListAsync(ct);

    public async Task<IEnumerable<StructuredSearchRow>> StructuredSearchAsync(int userId, string? title, string? plot, string? characters, string? person, CancellationToken ct = default)
        => await _db.CallStructuredStringSearch(userId, title, plot, characters, person).ToListAsync(ct);

    public async Task<IEnumerable<UserSearchHistoryRow>> GetUserSearchHistoryAsync(int userId, CancellationToken ct = default)
        => await _db.CallUserSearchHistory(userId).ToListAsync(ct);
}
