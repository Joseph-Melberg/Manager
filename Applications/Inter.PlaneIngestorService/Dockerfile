FROM mcr.microsoft.com/dotnet/runtime:6.0
COPY app App/
WORKDIR /App


RUN apt-get update \ 
  && apt-get install -y wget
LABEL deunhealth.restart.on.unhealthy: "true"
ENTRYPOINT ["dotnet", "Inter.PlaneIngestorService.dll"]

HEALTHCHECK  --interval=30s --timeout=3s --start-period=10s\
  CMD wget --no-verbose --tries=1 http://localhost:8180/health -O /dev/null || exit 1