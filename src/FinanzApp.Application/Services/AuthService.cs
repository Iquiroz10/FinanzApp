using FinanzApp.Application.DTOs.Auth;
using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;

namespace FinanzApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser is not null)
            throw new InvalidOperationException("Email already registered");

        var user = new Domain.Entities.User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = _passwordHasher.Hash(dto.Password)
        };

        var created = await _userRepository.AddAsync(user);
        var token = _tokenService.GenerateToken(created);

        return new AuthResponseDto
        {
            Token = token.Token,
            Name = created.Name,
            Email = created.Email,
            ExpiresAt = token.ExpiresAt
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email)
            ?? throw new UnauthorizedAccessException("Invalid credentials");

        if (!_passwordHasher.Verify(dto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        var token = _tokenService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token.Token,
            Name = user.Name,
            Email = user.Email,
            ExpiresAt = token.ExpiresAt
        };
    }
}