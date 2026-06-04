New-Item -Path "README.md" -ItemType "File" -Value @"
# 🧾 CashflowGateway - Enterprise Offline-First POS Backend Engine

<p align="left">
  <img src="https://shields.io" alt=".NET 10" />
  <img src="https://shields.io" alt="MySQL" />
  <img src="https://shields.io" alt="Clean Architecture" />
  <img src="https://shields.io" alt="Development Status" />
</p>

---

## 🚀 Project Overview

**CashflowGateway** serves as a high-performance, centralized cloud server engine specifically architected to manage multi-store, role-restricted Point of Sale (POS) operations. Designed with modern **.NET 10** using formal **Domain-Driven Design (DDD) Clean Architecture principles**, it acts as the data-synchronization core for local-first progressive web application (PWA) terminals operating in environment-unstable network conditions.

---

## 🏗️ Architectural Blueprint

The solution strictly isolates structural rules from external implementation systems across four distinct decoupled folder boundaries inside the `src/` directory:

```text
CashflowGateway/
├── 📁 src/
│   ├── 🔷 CashflowGateway.Domain/          # Pure Enterprise Rules (Zero dependencies)
│   ├── 🔷 CashflowGateway.Application/     # Orchestration Engine & Use Case Logic
│   ├── 🔷 CashflowGateway.Infrastructure/  # External Adapters (EF Core, MySQL Providers)
│   └── 🔷 CashflowGateway.API/             # Presentation Gateways (HTTP/REST Routing Controllers)
└── 🗃️ CashflowGateway.slnx                 # XML Solution Mapping Context Registry
```

### ⚙️ Operational Layer Boundaries
* **`Domain` Layer**: Houses your structural enterprise logic objects (`User.cs`, `Store.cs`, `Device.cs`, `Transaction.cs`) utilizing immutable structures and universal primary types (`Guid`).
* **`Application` Layer**: Coordinates core business system processing workflows via abstract structural mappings (`ISyncService.cs`) and structures incoming browser network contracts (`SyncPayloadDto.cs`).
* **`Infrastructure` Layer**: Manages data-access pipelines via Entity Framework Core context bridges (`AppDbContext.cs`), transforming loosely coupled entities onto hard lowercase MySQL schemas.
* **`API` Layer**: Encapsulates incoming public ports, hosting HTTP controllers (`SyncController.cs`), running registration injectors, and monitoring environment variable routes (`Program.cs`).

---

## 🔄 Automated Offline Synchronization Pipeline

To maintain high availability during complete internet connection loss, checkout registers interact with browser-native transaction ledgers (**IndexedDB**). When connectivity is restored, cached payloads are instantly batched and dispatched to this server's endpoint.

### 🛡️ Idempotence, Deduplication, & Conflict Detection Logic
To prevent network-jitter packet retries from corrupting store financial records, the server applies strict data validation constraints:

1. **Client-Driven Primaries**: Every transactional ticket utilizes a cryptographically secure browser-generated unique **UUID (`Guid`)**.
2. **Idempotency Inquiries**: Before initializing database inserts, the `SyncService` performs a non-blocking asynchronous record scan across index pools:
   ```csharp
   var exists = await _context.Set<Transaction>().AnyAsync(t => t.Id == txDto.Id);
   ```
3. **Graceful Duplicate Handling**: If a conflict is discovered, the server drops the duplicate log and returns a fast **`200 OK`**, signaling the terminal browser that it can safely clear its cache storage.

---

## 🛠️ Infrastructure Configuration & Deployment

### 📋 Prerequisites
* .NET 10 SDK (Standard compiler toolkit)
* MySQL Database Engine (Version 8.0 or newer)

### 🔑 Local Environment Keys
Configure your environment secrets and connection tokens inside `src/CashflowGateway.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=cashflow_gateway;User=root;Password=your_secure_password;"
  }
}
```

### 🔨 Compilation Assembly Run
Compile all decoupled solution project libraries cleanly using the .NET 10 assembly builder:
```bash
dotnet build CashflowGateway.slnx
```

### 🚀 Starting the Live Gateway Server
Boot the engine up to open public network communication lines and accept incoming frontend sync requests:
```bash
dotnet run --project src/CashflowGateway.API
```
"@ -Force
