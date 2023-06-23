FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
USER app
WORKDIR /app
EXPOSE 8003

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["Services/Discount/Discount.gRPC/Discount.gRPC.csproj", "Services/Discount/Discount.gRPC/"]
RUN dotnet restore "Services/Discount/Discount.gRPC/Discount.gRPC.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Discount.gRPC"
RUN dotnet build "Discount.gRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.gRPC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.gRPC.dll"]