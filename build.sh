#! /usr/bin/env bash

# Create the release
dotnet publish -c Release

# Build the image
docker build -t myimage -f Dockerfile .

# Run a container
docker run -it -p 5000:80 --rm myimage

