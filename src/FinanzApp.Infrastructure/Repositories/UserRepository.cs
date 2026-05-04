using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Domain.Entities;
using FinanzApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanzApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FinanzAppDbContext _context;

    public UserRepository(FinanzAppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(Guid id)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
}