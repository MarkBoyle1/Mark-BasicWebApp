#!/usr/bin/env bash

ktmpl "Jupiter/template.yaml" -f "Jupiter/defaults.yaml" --parameter imageTag ${BUILDKITE_BUILD_NUMBER} | kubectl apply -f -
