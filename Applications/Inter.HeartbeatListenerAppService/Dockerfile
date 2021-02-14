FROM mcr.microsoft.com/dotnet/aspnet:3.1
COPY . .
RUN ls
COPY app App/
WORKDIR /App
ENTRYPOINT ["dotnet", "Inter.HeartbeatListenerAppService.dll"]
