#!/usr/bin/env bash

# publish
time dotnet publish --output bin/Release/linux-x64 --runtime linux-x64 --self-contained --verbosity minimal
time dotnet publish --output bin/Release/win10-x64 --runtime win10-x64 --self-contained --verbosity minimal

# настроить Nginx как обратный прокси-сервер для перенаправления запросов в наше приложение ASP.NET Core
sudo cp -fv rss-station /etc/nginx/sites-available/rss-station
# проверить синтаксис файлов конфигурации
sudo nginx -t
# Если проверка файла конфигурации прошла успешно, заставьте Nginx принять изменения
sudo ln -fs /etc/nginx/sites-available/rss-station /etc/nginx/sites-enabled/rss-station
sudo nginx -s reload

# Сохраните файл и включите службу.
sudo cp -fv aspnetcore-rssstation.service /etc/systemd/system/
sudo systemctl enable aspnetcore-rssstation.service
# Запустите службу и убедитесь, что она работает.
sudo systemctl stop aspnetcore-rssstation.service
sudo systemctl start aspnetcore-rssstation.service
sudo systemctl status aspnetcore-rssstation.service
# Чтобы просмотреть элементы, связанные с kestrel-helloapp.service, используйте следующую команду.
sudo journalctl -fu aspnetcore-rssstation.service --since "2016-10-18" --until "2016-10-18 04:00"


# update
sudo systemctl stop aspnetcore-rssstation.service
killall RssStation
time dotnet publish --output bin/Release/linux-x64 --runtime linux-x64 --self-contained --verbosity minimal
sudo systemctl start aspnetcore-rssstation.service
sudo systemctl status aspnetcore-rssstation.service
