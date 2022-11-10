### How to start the system

1. Build docker images in Visual Studio
2. Start the API containers using following commands
```
docker run --name moviesapi1 -d -p 9001:80 -e "CONNECTION_STRING=Server=IPADDRESS;Database=movies1;User=sa;Password=Pass1234!;" moviesapi:latest
```
```
docker run --name moviesapi2 -d -p 9002:80 -e "CONNECTION_STRING=Server=IPADDRESS;Database=movies2;User=sa;Password=Pass1234!;" moviesapi:latest
```
3. Find the IP of the started containers inside docker
- Check the running containers
```docker ps```
- Find the IP address of the container
```docker inspect CONTAINERID | grep IPAddress```
- Inspect the bridge network (IP addresses can be found here too)
```docker inspect bridge```
4. Set the ip address of the APIs in ocelot.json
5. Build the docker image for the proxy
6. Start the proxy
```docker run -d -p 9999:80 proxy:latest```