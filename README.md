# WaitForDocker
This project helps with automating integration tests of services using docker. Instead of creating separate `docker-compose.yml` and `dockerfile` for each test project you can wait for infrastructure to be ready directly from code.

# Installation
```Install-Package WaitForDocker```
# Prerequisites
```docker-compose.yml``` which contains your infrastructure setup
# Usage
```await WaitForDocker.Compose()```
# Configuration
By default `WaitForDocker` will look for ```docker-compose.yml``` in current directory so marking this compose file as copy always is required. If you want to use different directory or compose file name you can use config builder.
``` var config = new WaitForDockerConfigurationBuilder()
                        .SetComposeDirectoryPath("path")
                        .SetCustomComposeFileName("filename")
                        .Build();```
