using FinanzApp.Application.DTOs.Auth;
using FinanzApp.Domain.Entities;

namespace FinanzApp.Application.Interfaces.Services;

public interface ITokenService
{
    (string Token, DateTime ExpiresAt) GenerateToken(User user);
}