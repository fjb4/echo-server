# Echo Server

A simple web application intended to be used in demonstrations.

Implemented in ASP.NET Core.

Build:
```
docker build -t fjb4/echo-server .
```

Run:
```
docker container run -it --rm -p 8000:80 fjb4/echo-server
```

Once running, you can view the output by executing `wget -qO- http://localhost:8000`.

Supported parameters:
- `text` allows you to specify the display text.
- `timestamp` displays the current time in UTC.
- `headers` displays the request headers.

Example:
```
docker container run -it --rm -p 8000:80 fjb4/echo-server --text="Welcome to Echo Server" --timestamp=true --headers=true
```

A ready-to-run container image is available on Docker Hub at https://hub.docker.com/repository/docker/fjb4/echo-server.
