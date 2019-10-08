FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

COPY . ./

RUN dotnet publish CESP.Service/CESP.Service.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/CESP.Service/out .
ENTRYPOINT ["dotnet", "CESP.Service.dll"]