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

    public Task AddAsync(int userId, string tconst, string? note, CancellationToken ct = default)
        => _repo.AddAsync(userId, tconst, note, ct);

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

    public Task DeleteAsync(int userId, string tconst, CancellationToken ct = default)
        => _repo.DeleteAsync(userId, tconst, ct);
}
