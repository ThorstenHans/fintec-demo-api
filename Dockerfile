FROM microsoft/dotnet:2.1-sdk AS builder
LABEL maintainer="Thorsten Hans<thorsten.hans@gmail.com>"

ARG CONFIGURATION=Debug
WORKDIR /app
ADD ./src/FintecDemo.API/ .
RUN dotnet publish -v m -c $CONFIGURATION -o /app/out

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=builder /app/out .

EXPOSE 80/tcp
CMD [ "dotnet", "FintecDemo.API.dll" ]
