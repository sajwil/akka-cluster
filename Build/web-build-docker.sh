#!/bin/bash

echo "building web image"
docker build -t web . -f ".\..\WebApp\Dockerfile"
