using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CashflowGateway.Domain;

namespace CashflowGateway.Application;

public class AuthService : IAuthService
{
    private readonly IAppDbContext    _context;
    private readonly IConfiguration  _config;

    public AuthService(IAppDbContext context, IConfiguration config)
    {
        _context = context;
        _config  = config;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request)
    {
     
        var existing = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (existing != null)
            return null; 

      
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

    
        var user = new User
        {
            Id           = Guid.NewGuid(),
            StoreId      = request.StoreId,
            FullName     = request.FullName,
            Email        = request.Email,
            PasswordHash = passwordHash,
            Role         = request.Role,
            CreatedAt    = DateTime.UtcNow
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return GenerateAuthResponse(user);
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
    {
       
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null)
            return null; 

      
        var passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!passwordValid)
            return null; 

       
        return GenerateAuthResponse(user);
    }

    private AuthResponseDto GenerateAuthResponse(User user)
    {
        var key        = new SymmetricSecurityKey(
                             Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds      = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiryHours = int.Parse(_config["Jwt:ExpiryHours"] ?? "8");
        var expiry     = DateTime.UtcNow.AddHours(expiryHours);


        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,   user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Role,               user.Role),
            new Claim("storeId",                     user.StoreId.ToString() ?? ""),
            new Claim("fullName",                    user.FullName ?? "")
        };

        var token = new JwtSecurityToken(
            issuer:   _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims:   claims,
            expires:  expiry,
            signingCredentials: creds
        );

        return new AuthResponseDto
        {
            Token     = new JwtSecurityTokenHandler().WriteToken(token),
            FullName  = user.FullName ?? "",
            Email     = user.Email    ?? "",
            Role      = user.Role,
            ExpiresAt = expiry
        };
    }
}