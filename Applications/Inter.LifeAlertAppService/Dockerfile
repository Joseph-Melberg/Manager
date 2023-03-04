FROM mcr.microsoft.com/dotnet/runtime:6.0
COPY app App/
WORKDIR /App
RUN apt-get update \ 
  && apt-get install -y wget
LABEL deunhealth.restart.on.unhealthy: "true"
ENTRYPOINT ["dotnet", "Inter.LifeAlertAppService.dll"]

HEALTHCHECK  --interval=30s --timeout=3s --start-period=10s\
  CMD wget --no-verbose --tries=1 --spider http://localhost/health || exit 1