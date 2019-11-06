FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Web/ProcessingTools.Web.Api.Documents/ProcessingTools.Web.Api.Documents.csproj", "Web/ProcessingTools.Web.Api.Documents/"]
RUN dotnet restore "Web/ProcessingTools.Web.Api.Documents/ProcessingTools.Web.Api.Documents.csproj"
COPY . .
WORKDIR "/src/Web/ProcessingTools.Web.Api.Documents"
RUN dotnet build "ProcessingTools.Web.Api.Documents.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProcessingTools.Web.Api.Documents.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProcessingTools.Web.Api.Documents.dll"]