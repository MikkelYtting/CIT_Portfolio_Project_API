using CIT_Portfolio_Project_API.Application.DTOs;
using CIT_Portfolio_Project_API.Models.Entities;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

public interface IPersonRepository
{
    Task<PageDto<PersonDto>> GetPeopleAsync(int page, int pageSize, CancellationToken ct = default);
    Task<Person?> GetByIdAsync(string nconst, CancellationToken ct = default);
}
