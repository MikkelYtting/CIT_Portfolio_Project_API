using AutoMapper;
using AutoMapper.QueryableExtensions;
using CIT_Portfolio_Project_API.Application.DTOs;
using CIT_Portfolio_Project_API.Infrastructure.Persistence;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;
using CIT_Portfolio_Project_API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Implementations;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    public MovieRepository(AppDbContext db, IMapper mapper)
    {
        _db = db; _mapper = mapper;
    }

    public async Task<PageDto<MovieDto>> GetMoviesAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var query = _db.Movies.AsQueryable();
        var total = await query.LongCountAsync(ct);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize)
            .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
        return new PageDto<MovieDto>
        {
            Page = page,
            PageSize = pageSize,
            Total = total,
            Items = items
        };
    }

    public async Task<Movie?> GetByIdAsync(string tconst, CancellationToken ct = default)
        => await _db.Movies.FirstOrDefaultAsync(m => m.Tconst == tconst, ct);

    public async Task<PageDto<MovieDto>> SearchAsync(string queryText, int page, int pageSize, CancellationToken ct = default)
    {
        // Delegate to DB function through SearchRepository in real implementation
        // TODO: Move to SearchRepository; kept simple here to compile
        var rows = await _db.CallStringSearch(0, queryText).ToListAsync(ct);
        var total = rows.Count;
        var items = rows.Skip((page - 1) * pageSize).Take(pageSize)
            .Select(r => new MovieDto { Tconst = r.Tconst, Title = r.Title });
        return new PageDto<MovieDto> { Page = page, PageSize = pageSize, Total = total, Items = items.ToList() };
    }

    public async Task<PageDto<MovieDto>> StructuredSearchAsync(string? title, string? plot, string? characters, string? person, int page, int pageSize, CancellationToken ct = default)
    {
        var rows = await _db.CallStructuredStringSearch(0, title, plot, characters, person).ToListAsync(ct);
        var total = rows.Count;
        var items = rows.Skip((page - 1) * pageSize).Take(pageSize)
            .Select(r => new MovieDto { Tconst = r.Tconst, Title = r.Title });
        return new PageDto<MovieDto> { Page = page, PageSize = pageSize, Total = total, Items = items.ToList() };
    }
}
