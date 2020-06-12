FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS builder

COPY . /
WORKDIR SuperDigital.Conta.Api
RUN dotnet restore --no-cache
RUN dotnet publish --output /app/ -c Release --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=builder /app .

ENV ASPNETCORE_ENVIRONMENT Development
ENV DOTNET_RUNNING_IN_CONTAINER true
ENV ASPNETCORE_URLS=http://*:80

EXPOSE 80
ENTRYPOINT ["dotnet", "SuperDigital.Conta.Api.dll"]