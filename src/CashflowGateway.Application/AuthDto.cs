using System;

namespace CashflowGateway.Application;

// What the client sends to register
public class RegisterRequestDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email    { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role     { get; set; } = "CASHIER"; // CASHIER, MANAGER, ADMIN
    public Guid?  StoreId  { get; set; }
}

// What the client sends to login
public class LoginRequestDto
{
    public string Email    { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

// What the server sends back after successful login
public class AuthResponseDto
{
    public string Token    { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email    { get; set; } = string.Empty;
    public string Role     { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}