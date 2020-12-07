#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Web/ProcessingTools.Web.Api.Biology.External.GbifV09/ProcessingTools.Web.Api.Biology.External.GbifV09.csproj", "Web/ProcessingTools.Web.Api.Biology.External.GbifV09/"]
RUN dotnet restore "Web/ProcessingTools.Web.Api.Biology.External.GbifV09/ProcessingTools.Web.Api.Biology.External.GbifV09.csproj"
COPY . .
WORKDIR "/src/Web/ProcessingTools.Web.Api.Biology.External.GbifV09"
RUN dotnet build "ProcessingTools.Web.Api.Biology.External.GbifV09.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProcessingTools.Web.Api.Biology.External.GbifV09.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProcessingTools.Web.Api.Biology.External.GbifV09.dll"]