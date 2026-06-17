using System;
using System.Threading.Tasks;

namespace CashflowGateway.Application;

public interface IReceiptService
{
    Task<ReceiptDto?> GetReceiptAsync(Guid transactionId);
}