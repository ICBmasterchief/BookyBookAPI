
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.sln .
COPY BookyBook.Models/*.csproj BookyBook.Models/
COPY BookyBook.Business/*.csproj BookyBook.Business/
COPY BookyBook.Data/*.csproj BookyBook.Data/
COPY BookyBook.API/*.csproj BookyBook.API/
RUN dotnet restore BookyBook.API/BookyBook.API.csproj

COPY . .
RUN dotnet publish BookyBookAPI.sln -c Release -o API/out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/API/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "BookyBook.API.dll"]
