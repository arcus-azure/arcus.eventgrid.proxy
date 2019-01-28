---
layout: default
title: Configuration
---

Here is an overview of how you can configure the container.

# Runtime
The runtime is flexible and allows you to configure it to meet your needs:
- `ARCUS_HTTP_PORT` - Defines the port to serve HTTP traffic _(default 80)_

# Azure Event Grid Topic
Proxy currently supports pushing events to only one Azure Event Grid topic.

The topic to push to must be configured with the following environment variables:
- `ARCUS_EVENTGRID_TOPICENDPOINT` - Url of the topic to push to
- `ARCUS_EVENTGRID_AUTHKEY` -  Authentication key to authenticate with

[&larr; back](/)