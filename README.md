# TelegramSellerBot

## Предварительные условия:
- Docker, docker-compose

## Настройка
Перед запуском необходимо создать .env файлы, которые должны распологаться по пути `deploy/environments/`

Необходимы два файла конфигурации:

```(Шаблон) postgres.env```
```
POSTGRES_USER: "имя пользователя postgres"
POSTGRES_HOST: "psql"
POSTGRES_PASSWORD: "пароль пользователя postgres"
PGDATA: "/var/lib/postgresql/data/pgdata"
```

```(Шаблон) secrets.env```

```
ConnectionStrings__TelegramIdentity="здесь строка подключения"
Telegram__ApiKey="здесь телеграм ключ"
```

## Запуск
Чтобы запустить приложение, необходимо перейти в папку `deploy/composes/` и выбрать директорию, с какой конфигурацией приложения запуститься (в нашем случае всего 1 конфигурация).
Переходим в директорию и прописываем:
```
docker-compose -f compose.dev.yml up
```
