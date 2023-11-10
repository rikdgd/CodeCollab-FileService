FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .
RUN dotnet build --configuration Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS release
WORKDIR /app
COPY --from=build /app/codecollab-backend/bin/Release .
EXPOSE 7049:7049 
RUN dotnet run
