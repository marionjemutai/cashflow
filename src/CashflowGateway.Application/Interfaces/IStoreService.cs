using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashflowGateway.Application;

public interface IStoreService
{
    Task<List<StoreResponseDto>> GetAllAsync();
    Task<StoreResponseDto?>      GetByIdAsync(Guid id);
    Task<StoreResponseDto>       CreateAsync(CreateStoreDto request);
    Task<StoreResponseDto?>      UpdateAsync(Guid id, UpdateStoreDto request);
}