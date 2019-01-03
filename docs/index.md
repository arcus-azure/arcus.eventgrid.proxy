---
title: "Home"
layout: default
permalink: /
redirect_from:
 - /index.html
---

**Arcus Event Grid Sidecar** is a container that makes it easier to integrate with Azure Event Grid.

It's a hosted REST API that allows you to POST events to which will be forwarded to an Azure Event Grid Topic of choice.

One of its main use cases is to run it as a sidecar next to your application but it can also be used as a standalone app in your infrastructure.

![Arcus Logo](https://raw.githubusercontent.com/arcus-azure/arcus/master/media/arcus.png)

# Installation
Running Arcus Event Grid Sidecar is super easy:
```
docker run -d -p 8999:80 --name arcus arcusazure/azure-event-grid-sidecar
```

Docker image is available on [Docker Hub](https://hub.docker.com/r/arcusazure/azure-event-grid-sidecar).

# Features
- Monitor runtime with a health endpoint ([docs](operations/health))

# Documentation
- **Configuration**
    - [Runtime](config#runtime)

# License
This is licensed under The MIT License (MIT). Which means that you can use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the web application. But you always need to state that Codit is the original author of this web application.

*[Full license here](https://github.com/arcus-azure/arcus.eventgrid.sidecar/blob/master/LICENSE)*
