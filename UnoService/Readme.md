docker build -t advanceduno/unoservice .
docker run -p 8000:8080 -d --name unoservice advanceduno/unoservice
docker push advanceduno/unoservice


