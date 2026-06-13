FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/Booking.Api/Booking.Api.csproj", "Booking.Api/"]
COPY ["src/Booking.Application/Booking.Application.csproj", "Booking.Application/"]
COPY ["src/Booking.Domain/Booking.Domain.csproj", "Booking.Domain/"]
COPY ["src/Booking.Infrastructure/Booking.Infrastructure.csproj", "Booking.Infrastructure/"]

RUN dotnet restore "Booking.Api/Booking.Api.csproj"

COPY src/ .

WORKDIR "/src/Booking.Api"
RUN dotnet publish "Booking.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Booking.Api.dll"]