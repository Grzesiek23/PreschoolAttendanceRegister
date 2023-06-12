# Stage 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ./PAR.API/PAR.API.csproj ./PAR.API/
COPY ./PAR.Application/PAR.Application.csproj ./PAR.Application/
COPY ./PAR.Contracts/PAR.Contracts.csproj ./PAR.Contracts/
COPY ./PAR.Domain/PAR.Domain.csproj ./PAR.Domain/
COPY ./PAR.Infrastructure/PAR.Infrastructure.csproj ./PAR.Infrastructure/
COPY ./PAR.Persistence/PAR.Persistence.csproj ./PAR.Persistence/

RUN dotnet restore "./PAR.API/PAR.API.csproj"
COPY ./PAR.API/ ./PAR.API/
COPY ./PAR.Application/ ./PAR.Application/
COPY ./PAR.Contracts/ ./PAR.Contracts/
COPY ./PAR.Domain/ ./PAR.Domain/
COPY ./PAR.Infrastructure/ ./PAR.Infrastructure/
COPY ./PAR.Persistence/ ./PAR.Persistence/
RUN dotnet publish "./PAR.API/PAR.API.csproj" -c Release -o /app/publish

# Stage 2 - Production
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PAR.API.dll"]
