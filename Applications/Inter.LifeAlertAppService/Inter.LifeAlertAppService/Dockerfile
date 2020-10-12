FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env
WORKDIR /src

RUN dotnet nuget locals all --clear

#RUN dotnet restore "Inter.LifeAlertAppService.csproj"

COPY . . 

FROM build-env AS publish
RUN dotnet publish "Inter.LifeAlertAppService.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "bin/Debug/netcoreapp3.1/publish/Inter.LifeAlertAppService.dll"]
