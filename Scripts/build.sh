#!/usr/bin/env bash

docker build -f ./BasicWebApp/Dockerfiles/Dockerfile -t docker.myob.com/future-makers-academy/mark-basic-web-app:${BUILDKITE_BUILD_NUMBER} .

#Push Docker Image to Cloudsmith
docker push docker.myob.com/future-makers-academy/mark-basic-web-app:${BUILDKITE_BUILD_NUMBER}