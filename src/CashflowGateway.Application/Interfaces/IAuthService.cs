using System.Threading.Tasks;

namespace CashflowGateway.Application;

public interface IAuthService
{
    Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
}