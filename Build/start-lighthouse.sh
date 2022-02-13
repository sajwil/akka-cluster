#!/bin/bash

docker stop lighthouse1
docker rm lighthouse1
docker run --name lighthouse1 -d --hostname lighthouse1 -p 4053:4053 -p 9110:9110 --env ACTORSYSTEM=Akka-Cluster --env CLUSTER_IP=localhost --env CLUSTER_PORT=4053 --env CLUSTER_SEEDS="akka.tcp://Akka-Cluster@localhost:4053" petabridge/lighthouse:latest

