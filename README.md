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

Or customize the display text with the `--text` parameter:
```
docker container run -it --rm -p 8000:80 fjb4/echo-server --text "Current UTC time is {{time}}"
```

Once running, you can view the output by running `wget -qO- http://localhost:8000`.
