# akka-cluster

### Build Engine Image
`docker build -t engine . -f ".\Engine\Dockerfile"`

### Build Web Image
`docker build -t web . -f ".\WebApp\Dockerfile"`

### Build Nginx Image
`docker build -t nginx . -f ".\Build\nginx\Dockerfile"`

### Destroy Cluster
`docker-compose down`

### Add Additional Engine
`docker run --name akka-cluster_engine4_1 -p 6004:9110 --env CLUSTER_SEEDS="akka.tcp://Akka-Cluster@lighthouse:4053" --env CLUSTER_PORT=5110 --network akka-cluster_default engine`

### References
https://getakka.net/articles/intro/what-is-akka.html
https://petabridge.com/blog/akkadotnet-what-is-an-actor/
https://www.lightbend.com/case-studies#filter%3Aakka
