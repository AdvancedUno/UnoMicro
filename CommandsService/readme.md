docker build -t advanceduno/commandservice .
docker push advanceduno/commandservice

docker run -p 8000:8080 advanceduno/commandservice -name commandservice