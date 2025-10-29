using AutoMapper;
using AutoMapper.QueryableExtensions;
using CIT_Portfolio_Project_API.Application.DTOs;
using CIT_Portfolio_Project_API.Infrastructure.Persistence;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;
using CIT_Portfolio_Project_API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Implementations;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    public PersonRepository(AppDbContext db, IMapper mapper)
    {
        _db = db; _mapper = mapper;
    }

    public async Task<PageDto<PersonDto>> GetPeopleAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var query = _db.Persons.AsQueryable();
        var total = await query.LongCountAsync(ct);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize)
            .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
        return new PageDto<PersonDto> { Page = page, PageSize = pageSize, Total = total, Items = items };
    }

    public Task<Person?> GetByIdAsync(string nconst, CancellationToken ct = default)
        => _db.Persons.FirstOrDefaultAsync(p => p.Nconst == nconst, ct);
}
