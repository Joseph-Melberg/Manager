FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as final
WORKDIR /
COPY / .
ENTRYPOINT ["dotnet", "bin/Debug/netcoreapp3.1/publish/Inter.LifeAlertAppService.dll"]
