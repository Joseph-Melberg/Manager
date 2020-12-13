FROM mcr.microsoft.com/dotnet/aspnet:3.1
COPY bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
EXPOSE 5000
ENTRYPOINT ["dotnet", "InterApi.dll"]
