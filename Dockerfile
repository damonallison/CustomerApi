FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy everything and build
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT [ "dotnet", "CustomerApi.dll"]

#
# IMPORTANT: This doesn't actually expose the port.
#
# To expose the port, use the `-p` argument.
# To expose container port 80 to host port 5000
# docker -it -p 5000:80 --rm customer-api
#
# You can now access the container on port 5000
# http://localhost:5000

EXPOSE 80
