
#!/bin/bash

echo "building engine image"
docker build -t engine . -f ".\Engine\Dockerfile"

