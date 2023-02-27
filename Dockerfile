#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["QuizPop/QuizPop.csproj", "QuizPop/"]
RUN dotnet restore "QuizPop/QuizPop.csproj"
COPY . .
WORKDIR "/src/QuizPop"
RUN dotnet build "QuizPop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuizPop.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizPop.dll"]