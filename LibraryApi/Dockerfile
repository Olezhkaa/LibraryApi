# Шаг 1: Базовый образ (За основу .net 8.0)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Шаг 2: Устанавливаем рабочую директорию
WORKDIR /app

# Шаг 3: Открываем порты
EXPOSE 80
EXPOSE 443

# Шаг 4: Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LibraryApi.csproj", "."]

# Шаг 5: Восстанавливаем зависимости
RUN dotnet restore "LibraryApi.csproj"

# Шаг 6: Копируем весь код и собираем
COPY . .
RUN dotnet build "LibraryApi.csproj" -c Release -o /app/build

# Шаг 7: Публикуем приложение
FROM build AS publish
RUN dotnet publish "LibraryApi.csproj" -c Release -o /app/publish

# Шаг 8: Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Шаг 9: Команда запуска
ENTRYPOINT ["dotnet", "LibraryApi.dll"]
