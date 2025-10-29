using CIT_Portfolio_Project_API.Models.Entities;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default);
    Task<User> AddAsync(User user, CancellationToken ct = default);
    Task<User?> GetByIdAsync(int id, CancellationToken ct = default);
}
