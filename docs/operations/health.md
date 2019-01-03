---
layout: default
title: Health
---

In order to operate the runtime we provide some tooling to automate the process.

## Health Endpoint
We expose a health endpoint that gives an indication whether or not the runtime is able to serve traffic.

You can easily query it with a simple GET:
```bash
curl -X GET "http://localhost:88/api/v1/health"
     -H "accept: application/json" --include
```

If everything is ok it will return an HTTP 200, otherwise an HTTP 503.

```bash
HTTP/1.1 200 OK
Date: Thu, 03 Jan 2019 12:50:36 GMT
Content-Length: 0
```

