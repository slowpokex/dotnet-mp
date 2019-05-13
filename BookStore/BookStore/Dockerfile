FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["BookStore/BookStore.csproj", "BookStore/"]
RUN dotnet restore "BookStore/BookStore.csproj"
COPY . .
WORKDIR "/src/BookStore"
RUN dotnet build "BookStore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BookStore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BookStore.dll"]