using System;

namespace CashflowGateway.Application;


public class CreateStoreDto
{
    public string  Name     { get; set; } = string.Empty;
    public string? Location { get; set; }
}


public class UpdateStoreDto
{
    public string? Name     { get; set; }
    public string? Location { get; set; }
}


public class StoreResponseDto
{
    public Guid      Id        { get; set; }
    public string    Name      { get; set; } = string.Empty;
    public string?   Location  { get; set; }
    public DateTime? CreatedAt { get; set; }
}