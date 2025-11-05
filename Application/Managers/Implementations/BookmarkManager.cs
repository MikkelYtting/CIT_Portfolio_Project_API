using AutoMapper;
using CIT_Portfolio_Project_API.Application.DTOs;
using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

namespace CIT_Portfolio_Project_API.Application.Managers.Implementations;

public class BookmarkManager : IBookmarkManager
{
    private readonly IBookmarkRepository _repo;
    private readonly IMapper _mapper;
    public BookmarkManager(IBookmarkRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

    /// <summary>
    /// Adds or updates a bookmark for the given user and movie (optional note supported).
    /// Ownership is enforced by passing the authenticated userId from the controller.
    /// </summary>
    public Task AddAsync(int userId, string tconst, string? note, CancellationToken ct = default)
        => _repo.AddAsync(userId, tconst, note, ct);

    /// <summary>
    /// Returns all bookmarks for the user and attaches a self link to the movie resource.
    /// </summary>
    public async Task<IEnumerable<BookmarkDto>> GetAsync(int userId, CancellationToken ct = default)
    {
        var rows = await _repo.GetAsync(userId, ct);
        return rows.Select(r => new BookmarkDto
        {
            Tconst = r.Tconst,
            Title = r.Title,
            Note = r.Note,
            Links = new List<LinkDto> { new("self", $"/api/movies/{r.Tconst}") }
        });
    }

    /// <summary>
    /// Removes a bookmark for the user; no-op if it doesn't exist.
    /// </summary>
    public Task DeleteAsync(int userId, string tconst, CancellationToken ct = default)
        => _repo.DeleteAsync(userId, tconst, ct);
}
