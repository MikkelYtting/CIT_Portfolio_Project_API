using CIT_Portfolio_Project_API.Application.DTOs;
using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

namespace CIT_Portfolio_Project_API.Application.Managers.Implementations;

public class SearchManager : ISearchManager
{
    private readonly ISearchRepository _repo;
    public SearchManager(ISearchRepository repo) { _repo = repo; }

    public async Task<PageDto<SearchDto>> StringSearchAsync(int userId, string text, int page, int pageSize, CancellationToken ct = default)
    {
        var rows = (await _repo.StringSearchAsync(userId, text, ct)).ToList();
        var total = rows.Count;
        var items = rows.Skip((page - 1) * pageSize).Take(pageSize)
            .Select(r => new SearchDto { Tconst = r.Tconst, Title = r.Title, Links = new List<LinkDto> { new("self", $"/api/movies/{r.Tconst}") } })
            .ToList();
        var dto = new PageDto<SearchDto> { Page = page, PageSize = pageSize, Total = total, Items = items };
        AddPageLinks(dto, $"/api/search?query={Uri.EscapeDataString(text)}");
        return dto;
    }

    public async Task<PageDto<SearchDto>> StructuredSearchAsync(int userId, string? title, string? plot, string? characters, string? person, int page, int pageSize, CancellationToken ct = default)
    {
        var rows = (await _repo.StructuredSearchAsync(userId, title, plot, characters, person, ct)).ToList();
        var total = rows.Count;
        var items = rows.Skip((page - 1) * pageSize).Take(pageSize)
            .Select(r => new SearchDto { Tconst = r.Tconst, Title = r.Title, Links = new List<LinkDto> { new("self", $"/api/movies/{r.Tconst}") } })
            .ToList();
        var basePath = $"/api/search/structured?title={Uri.EscapeDataString(title ?? string.Empty)}&plot={Uri.EscapeDataString(plot ?? string.Empty)}&characters={Uri.EscapeDataString(characters ?? string.Empty)}&person={Uri.EscapeDataString(person ?? string.Empty)}";
        var dto = new PageDto<SearchDto> { Page = page, PageSize = pageSize, Total = total, Items = items };
        AddPageLinks(dto, basePath);
        return dto;
    }

    private static void AddPageLinks<T>(PageDto<T> dto, string basePath)
    {
        dto.Links.Add(new LinkDto("self", $"{basePath}&page={dto.Page}&pageSize={dto.PageSize}"));
        if (dto.Page > 1)
            dto.Links.Add(new LinkDto("prev", $"{basePath}&page={dto.Page - 1}&pageSize={dto.PageSize}"));
        var totalPages = (int)Math.Ceiling(dto.Total / (double)dto.PageSize);
        if (dto.Page < Math.Max(totalPages, 1))
            dto.Links.Add(new LinkDto("next", $"{basePath}&page={dto.Page + 1}&pageSize={dto.PageSize}"));
    }
}
