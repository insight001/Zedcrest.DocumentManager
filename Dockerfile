# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.sln .
COPY *.csproj .
COPY Zedcrest.DocumentManager.UnitTests/Zedcrest.DocumentManager.UnitTests.csproj Zedcrest.DocumentManager.UnitTests/
RUN dotnet restore
COPY . .


# publish
FROM build AS publish
WORKDIR /src
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Zedcrest.DocumentManager.dll
