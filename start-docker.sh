#! /usr/bin/env bash

echo -e "Starting docker FinTec Demo API docker container"

docker run -d -p ${1-8080}:80/tcp --env="ASPNETCORE_ENVIRONMENT=Development" fintec-demo-api:development-latest

echo -e "Docker container started"

