# WaitForDocker
This project helps with automating integration tests of services using docker. It will run your `docker-compose.yml` infrastructure file and will wait until your services will be ready to receive calls. After finished tests, you can easily remove your services by killing them.

# Installation
```Install-Package WaitForDocker```
# Prerequisites
```docker-compose.yml``` which contains your infrastructure setup
# Usage
```await WaitForDocker.Compose()```
# Configuration
`WaitForDocker` will look for ```docker-compose.yml``` in current directory so marking this compose file as copy always is required. If you want to use different directory or compose file name you can use config builder.
``` 
var config = new WaitForDockerConfigurationBuilder()
                 .SetComposeDirectoryPath("path")
                 .SetCustomComposeFileName("filename")
                 .Build();
```
# Health checking
By default `WaitForDocker` will read `services` tag and they `ports` from `docker-compose.yml` and check that port is open.
```
services:
  redis:
    image: redis
    ports:
      - '6379:6379'
```

# Logging
For setup custom logger you have to implement `ILogger` interface and pass it to config. By default logs will be printed to console.
```
var config = new WaitForDockerConfigurationBuilder()
                .SetCustomLogger(new FileOutputLogger())
                .Build();
```
