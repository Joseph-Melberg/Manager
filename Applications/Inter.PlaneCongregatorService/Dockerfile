FROM mcr.microsoft.com/dotnet/runtime:6.0
COPY app App/
WORKDIR /App
ENTRYPOINT ["dotnet", "Inter.PlaneCongregatorService.dll"]

HEALTHCHECK  --interval=30s --timeout=3s --start-period=10s\
  CMD wget --no-verbose --tries=1 --spider http://localhost/health || exit 1