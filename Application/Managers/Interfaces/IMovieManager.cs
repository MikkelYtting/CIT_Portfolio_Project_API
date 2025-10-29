using CIT_Portfolio_Project_API.Application.DTOs;

namespace CIT_Portfolio_Project_API.Application.Managers.Interfaces;

public interface IMovieManager
{
    Task<PageDto<MovieDto>> GetMoviesAsync(int page, int pageSize, CancellationToken ct = default);
    Task<MovieDto?> GetByIdAsync(string tconst, CancellationToken ct = default);
    Task<PageDto<MovieDto>> SearchAsync(string query, int page, int pageSize, CancellationToken ct = default);
    Task<PageDto<MovieDto>> StructuredSearchAsync(string? title, string? plot, string? characters, string? person, int page, int pageSize, CancellationToken ct = default);
}
