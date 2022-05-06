#!/usr/bin/env bash

docker build -f ./BasicWebApp/Dockerfiles/Dockerfile.test -t basic-web-app-tests .
docker run basic-web-app-tests