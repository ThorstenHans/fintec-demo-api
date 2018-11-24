#! /usr/bin/env bash

echo -e "Building Docker image for FinTec Demo API"

docker build --build-arg CONFIGURATION=Debug . -t fintec-demo-api:development-latest > /dev/null

echo -e "Successfully built Docker image fintec-demo-api:development-latest"
