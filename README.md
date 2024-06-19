# UnoMicro Microservice

UnoMicro is a microservice built using C# and .NET 8, designed to demonstrate microservice architecture with Docker, Kubernetes, HTTP, gRPC, and Microsoft SQL Server.

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- **.NET 8**: Built with the latest version of .NET.
- **Docker**: Containerized application for easy deployment.
- **Kubernetes**: Orchestrated deployment and management of containers.
- **HTTP and gRPC**: Supports both HTTP and gRPC protocols.
- **Microsoft SQL Server**: Uses MS SQL Server for database management.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Kubernetes](https://kubernetes.io/docs/tasks/tools/)
- [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/)
- [Helm](https://helm.sh/docs/intro/install/)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/yourusername/UnoMicro.git
cd UnoMicro
```

### Build and Run with Docker

1. **Build Docker Image**

   ```bash
   docker build -t unomicro:latest .
  ```

2. **Run Docker Container**

   ```bash
   docker run -d -p 8000:8080 -p 443:443 -p 5000:5000 -p 5001:5001 --name unomicro unomicro:latest
  ```

### Deploy to Kubernetes

1. **Create Kubernetes Secret for SQL Server Password**

   ```bash
   kubectl create secret generic mssql-secret --from-literal=SA_PASSWORD='StrongPassw0rd!'
   ```

2. **Apply Kubernetes Manifests**

   ```bash
   kubectl apply -f kubernetes/
   ```

3. **Verify Deployment**

   ```bash
   kubectl get pods
   kubectl get services
   ```

### Configuration

Ensure your \`appsettings.Production.json\` is correctly set up:

```json
{
    "CommandService": "http://commands-clusterip-srv:8080/api/c/uno/",
    "ConnectionStrings": {
        "UnoConn": "Server=mssql-clusterip-srv,1433;Initial Catalog=unodb;User ID=sa;Password=StrongPassw0rd!"
    }
}
```

### Environment Variables

Ensure you have set the environment variable for the production environment in your Kubernetes deployment YAML:

```yaml
env:
  - name: ASPNETCORE_ENVIRONMENT
    value: "Production"
```


## Usage

### Running the Application

- **HTTP**: Access the HTTP service at \`http://localhost:8080\`.
- **gRPC**: Access the gRPC service at \`http://localhost:5000\`.

### Migrations

To apply migrations, ensure you have the \`dotnet-ef\` tool installed and run:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialMigration
dotnet ef database update
```

### Program.cs Configuration

Ensure your \`Program.cs\` reads the configuration correctly:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var Configuration = builder.Configuration;

Console.WriteLine($"--> CommandService Endpoint {Configuration["CommandService"]}");
Console.WriteLine($"--> ConnectionString: {Configuration.GetConnectionString("UnoConn")}");

if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("--> Using SqlServer Db");
    builder.Services.AddDbContext<AppDbContext>(opt => 
        opt.UseSqlServer(Configuration.GetConnectionString("UnoConn")));
}
else
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt => 
        opt.UseInMemoryDatabase("InMem"));
}

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnoRepo, UnoRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed the database
PrepDb.PrepPopulation(app);

app.Run();
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
