using CIT_Portfolio_Project_API.Infrastructure.Persistence;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;
using CIT_Portfolio_Project_API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CIT_Portfolio_Project_API.Infrastructure.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default)
        => await _db.Users.FirstOrDefaultAsync(x => x.Username == username, ct);

    public async Task<User> AddAsync(User user, CancellationToken ct = default)
    {
        // Delegate creation to DB function to enforce Part 1 rules
        await _db.ExecuteCreateUserAsync(user.Username, user.Email, user.PasswordHash, ct);
        // Attempt to re-load the created user by username
        var created = await _db.Users.FirstOrDefaultAsync(x => x.Username == user.Username, ct);
        return created ?? user;
    }

    public Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
        => _db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
}
