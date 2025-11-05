using CIT_Portfolio_Project_API.Application.DTOs;
using CIT_Portfolio_Project_API.Models.Entities;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

public interface IMovieRepository
{
    Task<PageDto<MovieDto>> GetMoviesAsync(int page, int pageSize, CancellationToken ct = default);
    Task<Movie?> GetByIdAsync(string tconst, CancellationToken ct = default);
}
