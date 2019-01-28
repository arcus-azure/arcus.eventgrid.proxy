---
layout: default
title: Push Events
---

You can easily push events to your topic via the proxy:
```bash
curl -X POST "http://localhost:8999/api/v1/events/Arcus.NewCarRegistered"
     -H "accept: text/plain" -H "Content-Type: application/json-patch+json" 
     -d "{ \"licensePlate\": \"1-ABC-337\"}" --include
```

If everything is ok it will return an HTTP 204, otherwise an HTTP 503.

```bash
HTTP/1.1 204 No Content
Date: Mon, 28 Jan 2019 17:49:43 GMT
X-Event-Id: e9ca21cc-50ed-4138-a760-1ca9b01b092f
X-Event-Subject: /
X-Event-Timestamp: 2019-01-28T17:49:43.9021889+00:00
X-Event-Data-Version: 1.0
```

## Optional parameters
We provide the capability to be more specific about your events.

You can specify more information with the following query parameters:
- `eventId` - Unique id for your event _(default: Guid)_
- `eventTimestamp` - Timestamp of event _(default: UTC)_
- `eventSubject` - Subject of event _(default: /)_
- `dataVersion` - Version of data payload _(default: 1.0)_

[&larr; back](/)