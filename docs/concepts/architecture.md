---
layout: default
title: Using Arcus Event Grid Sidecar
---

# Architecture
We provide a hosted REST API that allows you to POST events to which will be forwarded to an Azure Event Grid Topic of choice.

![Concept Overview](./../media/concepts-architecture.png)

One of its main use cases is to run it as a sidecar next to your application but it can also be used as a standalone app in your infrastructure.
