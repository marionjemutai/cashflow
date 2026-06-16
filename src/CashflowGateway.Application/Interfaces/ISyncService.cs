using System;
using System.Threading.Tasks;

namespace CashflowGateway.Application;

public interface ISyncService
{
    Task<bool> ProcessSyncPayloadAsync(SyncPayloadDto payload);

    Task<SyncPullResponseDto> GetPullDataAsync(Guid deviceId, DateTime since);
}