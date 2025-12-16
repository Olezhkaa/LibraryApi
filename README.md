# ***LibraryApi***
# ***Система управления библиотекой***

<p align="center">
<img src="https://github.com/Olezhkaa/LibraryApi/blob/main/images/image.png" width="50%" height="50%">
</p>

## **ЗАПУСК И ОСТАНОВКА**
docker-compose up -d              # Запустить

docker-compose down               # Остановить

docker-compose down -v            # Остановить с удалением БД

docker-compose restart            # Перезапустить

docker-compose stop               # Только остановить

docker-compose ps                 # Показать статус

## **ПЕРЕСБОРКА**
docker-compose down               # Остановить

docker-compose build --no-cache   #Собрать

docker-compose up -d              #Запустить

## **ЛОГИ**
docker-compose logs -f            # Все логи

docker-compose logs -f api        # Логи API

docker-compose logs -f postgres   # Логи БД

## **БАЗА ДАННЫХ**
dotnet ef database update name                                      # Миграция

dotnet ef database update AddChangeAuthorFixDate                    # Миграция (Последняя на данный)

psql -h localhost -p 5433 -U postgres -d LibraryDB                  # Подключиться к БД (пароль: admin)

## **ТЕСТИРОВАНИЕ**
curl http://localhost:5000/health                                   # Проверить API

start http://localhost:5000/                                        # Открыть в браузере

start http://localhost:5000/swagger                                 # Открыть Swagger

## **ДИАГНОСТИКА**
docker exec -it library-api sh                                      # Войти в API контейнер

docker exec -it library-postgres bash                               # Войти в БД контейнер

netstat -ano | findstr :5000                                        # Проверить порт API

netstat -ano | findstr :5433                                        # Проверить порт БД

## **ОЧИСТКА**
docker-compose down -v                                              # Удалить всё

docker system prune -a --volumes                                    # Полная очистка Docker

## **ЕСЛИ НЕ РАБОТАЕТ**
docker restart library-api                                          # Перезапустить API

docker restart library-postgres                                     # Перезапустить БД

docker exec library-api dotnet ef migrations list                   # Проверить миграции
