#!/bin/bash
dotnet restore
dotnet publish -c Release
docker build -t docker.centurionx.net/interapi -f Dockerfile .
 
