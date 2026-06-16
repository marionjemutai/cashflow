using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashflowGateway.Application;

public interface ILedgerService
{
    Task<List<LedgerEntryDto>> GetLedgerAsync(DateTime? from, DateTime? to);
    Task<LedgerSummaryDto>     GetSummaryAsync(DateTime? from, DateTime? to);
}