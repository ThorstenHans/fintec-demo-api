#! /usr/bin/env bash
if [ ! -d "src/FintecDemo.API/bin/Debug" ]; then
    dotnet build -c Debug src/FintecDemo.API/FintecDemo.API.csproj
fi
dotnet src/FintecDemo.API/bin/Debug/netcoreapp2.1/FintecDemo.API.dll

