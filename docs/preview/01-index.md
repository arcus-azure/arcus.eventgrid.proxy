---
title: "Arcus Event Grid Proxy"
layout: default
permalink: /
slug: /
sidebar_label: Welcome
redirect_from:
 - /index.html
---

**Arcus Event Grid Proxy** is a container that makes it easier to integrate with Azure Event Grid.

![Arcus Logo](https://raw.githubusercontent.com/arcus-azure/arcus/master/media/arcus.png)

# Installation
Running Arcus Event Grid Proxy is super easy:
```
docker run -d -p 8999:80 --name arcus arcusazure/arcus-event-grid-proxy /
                         -e ARCUS_EVENTGRID_TOPICENDPOINT= /
                         -e ARCUS_EVENTGRID_AUTHKEY=
```

Docker image will be available on [Docker Hub](https://hub.docker.com/r/arcusazure/arcus-event-grid-proxy).

# Features
- Push events to Azure Event Grid via REST ([docs](features/push-events))
- Monitor runtime with a health endpoint ([docs](operations/health))

# Documentation
- **Concepts**
    - [Using Arcus Event Grid Proxy](concepts/architecture)
- **Deployment**
    - [Image Tagging Strategy](deploy/tagging-strategy)
- **Configuration**
    - [Runtime](config#runtime)
    - [Azure Event Grid Topic](config#azure-event-grid-topic)

# License
This is licensed under The MIT License (MIT). Which means that you can use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the web application. But you always need to state that Codit is the original author of this web application.

*[Full license here](https://github.com/arcus-azure/arcus.eventgrid.proxy/blob/master/LICENSE)*
