FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

RUN dotnet publish "BookyBook.API" -c Release -o /BookyBook

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /BookyBookAPI
COPY --from=build /src .

EXPOSE 7790
VOLUME /BookyBook/BookyBook.Data

ENV MACHINE_NAME ${COMPUTERNAME}
ENTRYPOINT ["dotnet", "BookyBook.API/bin/Release/net6.0/BookyBook.API.dll"]
