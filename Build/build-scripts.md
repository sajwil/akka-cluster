### Build Engine Image
`docker build -t engine . -f ".\Engine\Dockerfile"`

### Build Web Image
`docker build -t web . -f ".\WebApp\Dockerfile"`

### Scaling
`docker-compose scale lighthouse=1 engine=3 web=1`

### Destroy Cluster
`docker-compose down`

### Docker Logs in Each Engine
`docker logs build_engine0_1`
`docker logs build_engine1_1`
`docker logs build_engine2_1`

### Docker Logs in Each Web
`docker logs build_web0_1`
`docker logs build_web1_1`
`docker logs build_web2_1`