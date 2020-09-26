#Set Environment
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env
WORKDIR /app
RUN ["pwd"]
# Copy csproj and restore as distinct layers
COPY ../Inter.HeartbeatListenerAppService/*.csproj resultant/

COPY ../../Inter.Domain/*.csproj Domain/
COPY ../Inter.Domain/*.csproj Domain/