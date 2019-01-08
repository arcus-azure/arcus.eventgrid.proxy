---
title: "Home"
layout: default
permalink: /
redirect_from:
 - /index.html
---

**Arcus Event Grid Sidecar** is a container that makes it easier to integrate with Azure Event Grid.

![Arcus Logo](https://raw.githubusercontent.com/arcus-azure/arcus/master/media/arcus.png)

# Installation
Running Arcus Event Grid Sidecar is super easy:
```
docker run -d -p 8999:80 --name arcus arcusazure/azure-event-grid-sidecar
```

Docker image will be available on [Docker Hub](https://hub.docker.com/r/arcusazure/azure-event-grid-sidecar).

# Features
- Monitor runtime with a health endpoint ([docs](operations/health))

# Documentation
- **Concepts**
    - [Using Arcus Event Grid Sidecar](concepts/architecture)
- **Deployment**
    - [Image Tagging Strategy](deploy/tagging-strategy)
- **Configuration**
    - [Runtime](config#runtime)    

# License
This is licensed under The MIT License (MIT). Which means that you can use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the web application. But you always need to state that Codit is the original author of this web application.

*[Full license here](https://github.com/arcus-azure/arcus.eventgrid.sidecar/blob/master/LICENSE)*
