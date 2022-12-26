
DB_DIR=./Docker_DB
WEB_PROJ=dotnet7_webapi
WEB_PROJ_DIR=./DotNet7_WebAPI/DotNet7_WebAPI/DotNet7_WebAPI.csproj
WEB_BUILD_DIR=WebServerBuild

ALL : up
up : down
	-docker image rm $(WEB_PROJ):1.0.0
	dotnet publish $(WEB_PROJ_DIR) --output $(WEB_BUILD_DIR) --os linux --arch x64 -c Release -p:PublishProfile=DefaultContainer
	docker compose -f $(DB_DIR)/docker-compose.yml up -d

down :
	-docker compose -f $(DB_DIR)/docker-compose.yml down

re : rm up

rm :
	-docker compose -f $(DB_DIR)/docker-compose.yml down --rmi all 
