# WaitForDocker
This project helps with automating integration tests of services using docker. Instead of creating separate `docker-compose.yml` and `dockerfile` for each test project you can wait for infrastructure to be ready directly from code.

# Installation
```Install-Package WaitForDocker```
# Prerequisites
```docker-compose.yml``` which contains you infrastructure setup
# Usage
```await WaitForDocker.Compose()```
