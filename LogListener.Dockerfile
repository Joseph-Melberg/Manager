FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env
WORKDIR /src

RUN dotnet nuget locals all --clear

COPY ["Applications/Inter.LogRecieverAppService/Inter.LogRecieverAppService.csproj", "./Applications/Inter.LogRecieverAppService/"]

COPY ["Inter.Domain/Inter.Domain.csproj", "Inter.Domain/"]
COPY ["Inter.DomainServices/Inter.DomainServices.csproj", "Inter.DomainServices/"]
COPY ["Inter.DomainServices.Core/Inter.DomainServices.Core.csproj", "Inter.DomainServices.Core/"]
COPY ["Inter.Infrastructure/Inter.Infrastructure.csproj", "Inter.Infrastructure/"]
COPY ["Inter.Infrastructure.Core/Inter.Infrastructure.Core.csproj", "Inter.Infrastructure.Core/"]
COPY ["Inter.Infrastructure.MySQL/Inter.Infrastructure.MySQL.csproj", "Inter.Infrastructure.MySQL/"]
COPY ["Inter.DomainServices.Core/Inter.DomainServices.Core.csproj", "Inter.DomainServices.Core/"]
COPY ["Inter.Infrastructure.Core/Inter.Infrastructure.Core.csproj", "Inter.Infrastructure.Core/"]
COPY ["Inter.DomainServices.Core/Inter.DomainServices.Core.csproj", "Inter.DomainServices.Core/"]
COPY ["Inter.Infrastructure/Inter.Infrastructure.csproj", "Inter.Infrastructure/"]
COPY ["Inter.Infrastructure.Core/Inter.Infrastructure.Core.csproj", "Inter.Infrastructure.Core/"]
COPY ["Inter.Infrastructure.MySQL/Inter.Infrastructure.MySQL.csproj", "Inter.Infrastructure.MySQL/"]

RUN dotnet restore "./Applications/Inter.LogRecieverAppService/Inter.LogRecieverAppService.csproj"

COPY . .

WORKDIR "/src/Applications/Inter.LogRecieverAppService"
RUN ls
RUN dotnet build "Inter.LogRecieverAppService.csproj" -c Release -o /app

FROM build-env AS publish
RUN dotnet publish "Inter.LogRecieverAppService.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Inter.LogRecieverAppService.dll"]
