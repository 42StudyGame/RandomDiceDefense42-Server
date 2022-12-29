
DB_DIR=./Docker_DB
WEB_PROJ=dotnet7_webapi
WEB_PROJ_DIR=./DotNet7_WebAPI/DotNet7_WebAPI/DotNet7_WebAPI.csproj
WEB_BUILD_DIR=WebServerBuild

ALL : up
up :
	docker compose -f $(DB_DIR)/docker-compose.yml up --build --force-recreate -d

web :
	dotnet publish $(WEB_PROJ_DIR) --output $(WEB_BUILD_DIR) --os linux --arch x64 -c Release -p:PublishProfile=DefaultContainer

down :
	-docker compose -f $(DB_DIR)/docker-compose.yml down -v

re : rm up

rm :
	-docker compose -f $(DB_DIR)/docker-compose.yml down --rmi all 
