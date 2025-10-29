using CIT_Portfolio_Project_API.Application.DTOs;

namespace CIT_Portfolio_Project_API.Application.Managers.Interfaces;

public interface IBookmarkManager
{
    Task AddAsync(int userId, string tconst, string? note, CancellationToken ct = default);
    Task<IEnumerable<BookmarkDto>> GetAsync(int userId, CancellationToken ct = default);
    Task DeleteAsync(int userId, string tconst, CancellationToken ct = default);
}
