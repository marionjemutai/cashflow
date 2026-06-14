using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashflowGateway.Application;

public interface IInventoryService
{
    Task<List<ProductResponseDto>> GetProductsAsync(Guid? storeId);
    Task<ProductResponseDto?>      GetProductByIdAsync(Guid id);
    Task<ProductResponseDto>       CreateProductAsync(CreateProductDto request);
    Task<ProductResponseDto?>      UpdateProductAsync(Guid id, UpdateProductDto request);
    Task<bool>                     DeactivateProductAsync(Guid id);
    Task<List<InventoryMovementResponseDto>> GetMovementsAsync(Guid productId);
}