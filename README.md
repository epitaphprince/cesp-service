# cesp-service

## build docker image
```
docker build . -t cr.yandex/crpi40k5e5q63h0a89q1/cesp-service
```

## Push Docker image
```
docker push cr.yandex/crpi40k5e5q63h0a89q1/cesp-service
```

## Pull Docker image on remote machine
```
docker pull cr.yandex/crpi40k5e5q63h0a89q1/cesp-service
```

docker-compose up -d

## Run Docker container
```
docker run -d -p 81:80 --restart always --name cesp-service cr.yandex/crpi40k5e5q63h0a89q1/cesp-service 
```
