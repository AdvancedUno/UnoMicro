docker build -t advanceduno/unoservice .
docker push advanceduno/unoservice
docker run -p 8000:8080 -d --name unoservice advanceduno/unoservice


dotnet ef migrations add [name of the migration]
