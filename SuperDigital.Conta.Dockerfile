FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app


# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -p:PublishDir=out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app

COPY --from=build-env /app/SuperDigital.Conta.Api/out ./superdigital_conta_api
RUN ls


ENTRYPOINT ["dotnet", "SuperDigital.Conta.Api.dll"]