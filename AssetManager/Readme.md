---
title: AssetManager v1.0
language_tabs:
  - shell: Shell
  - http: HTTP
  - javascript: JavaScript
  - ruby: Ruby
  - python: Python
  - php: PHP
  - java: Java
  - go: Go
toc_footers: []
includes: []
search: true
highlight_theme: darkula
headingLevel: 2

---

<!-- Generator: Widdershins v4.0.1 -->

<h1 id="assetmanager">AssetManager v1.0</h1>

> Scroll down for code samples, example requests and responses. Select a language for code samples from the tabs above or the mobile navigation menu.

<h1 id="assetmanager-content">Content</h1>

## get__Content_{guid}

> Code samples

```shell
# You can also use wget
curl -X GET /Content/{guid} \
  -H 'Accept: text/plain'

```

```http
GET /Content/{guid} HTTP/1.1

Accept: text/plain

```

```javascript

const headers = {
  'Accept':'text/plain'
};

fetch('/Content/{guid}',
{
  method: 'GET',

  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

```ruby
require 'rest-client'
require 'json'

headers = {
  'Accept' => 'text/plain'
}

result = RestClient.get '/Content/{guid}',
  params: {
  }, headers: headers

p JSON.parse(result)

```

```python
import requests
headers = {
  'Accept': 'text/plain'
}

r = requests.get('/Content/{guid}', headers = headers)

print(r.json())

```

```php
<?php

require 'vendor/autoload.php';

$headers = array(
    'Accept' => 'text/plain',
);

$client = new \GuzzleHttp\Client();

// Define array of request body.
$request_body = array();

try {
    $response = $client->request('GET','/Content/{guid}', array(
        'headers' => $headers,
        'json' => $request_body,
       )
    );
    print_r($response->getBody()->getContents());
 }
 catch (\GuzzleHttp\Exception\BadResponseException $e) {
    // handle exception or api errors.
    print_r($e->getMessage());
 }

 // ...

```

```java
URL obj = new URL("/Content/{guid}");
HttpURLConnection con = (HttpURLConnection) obj.openConnection();
con.setRequestMethod("GET");
int responseCode = con.getResponseCode();
BufferedReader in = new BufferedReader(
    new InputStreamReader(con.getInputStream()));
String inputLine;
StringBuffer response = new StringBuffer();
while ((inputLine = in.readLine()) != null) {
    response.append(inputLine);
}
in.close();
System.out.println(response.toString());

```

```go
package main

import (
       "bytes"
       "net/http"
)

func main() {

    headers := map[string][]string{
        "Accept": []string{"text/plain"},
    }

    data := bytes.NewBuffer([]byte{jsonReq})
    req, err := http.NewRequest("GET", "/Content/{guid}", data)
    req.Header = headers

    client := &http.Client{}
    resp, err := client.Do(req)
    // ...
}

```

`GET /Content/{guid}`

*Returns the file located under the guid returned by the File endpoints*

<h3 id="get__content_{guid}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|guid|path|string(uuid)|true|The guid of the sought after file|

> Example responses

> 200 Response

```
{"canTimeout":true,"readTimeout":0,"writeTimeout":0,"handle":{},"canRead":true,"canWrite":true,"safeFileHandle":{"isClosed":true,"isInvalid":true,"isAsync":true},"name":"string","isAsync":true,"length":0,"position":0,"canSeek":true}
```

```json
{
  "canTimeout": true,
  "readTimeout": 0,
  "writeTimeout": 0,
  "handle": {},
  "canRead": true,
  "canWrite": true,
  "safeFileHandle": {
    "isClosed": true,
    "isInvalid": true,
    "isAsync": true
  },
  "name": "string",
  "isAsync": true,
  "length": 0,
  "position": 0,
  "canSeek": true
}
```

<h3 id="get__content_{guid}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[FileStream](#schemafilestream)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="assetmanager-file">File</h1>

## post__File

> Code samples

```shell
# You can also use wget
curl -X POST /File \
  -H 'Content-Type: multipart/form-data' \
  -H 'Accept: text/plain'

```

```http
POST /File HTTP/1.1

Content-Type: multipart/form-data
Accept: text/plain

```

```javascript
const inputBody = '{
  "files": [
    "string"
  ]
}';
const headers = {
  'Content-Type':'multipart/form-data',
  'Accept':'text/plain'
};

fetch('/File',
{
  method: 'POST',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

```ruby
require 'rest-client'
require 'json'

headers = {
  'Content-Type' => 'multipart/form-data',
  'Accept' => 'text/plain'
}

result = RestClient.post '/File',
  params: {
  }, headers: headers

p JSON.parse(result)

```

```python
import requests
headers = {
  'Content-Type': 'multipart/form-data',
  'Accept': 'text/plain'
}

r = requests.post('/File', headers = headers)

print(r.json())

```

```php
<?php

require 'vendor/autoload.php';

$headers = array(
    'Content-Type' => 'multipart/form-data',
    'Accept' => 'text/plain',
);

$client = new \GuzzleHttp\Client();

// Define array of request body.
$request_body = array();

try {
    $response = $client->request('POST','/File', array(
        'headers' => $headers,
        'json' => $request_body,
       )
    );
    print_r($response->getBody()->getContents());
 }
 catch (\GuzzleHttp\Exception\BadResponseException $e) {
    // handle exception or api errors.
    print_r($e->getMessage());
 }

 // ...

```

```java
URL obj = new URL("/File");
HttpURLConnection con = (HttpURLConnection) obj.openConnection();
con.setRequestMethod("POST");
int responseCode = con.getResponseCode();
BufferedReader in = new BufferedReader(
    new InputStreamReader(con.getInputStream()));
String inputLine;
StringBuffer response = new StringBuffer();
while ((inputLine = in.readLine()) != null) {
    response.append(inputLine);
}
in.close();
System.out.println(response.toString());

```

```go
package main

import (
       "bytes"
       "net/http"
)

func main() {

    headers := map[string][]string{
        "Content-Type": []string{"multipart/form-data"},
        "Accept": []string{"text/plain"},
    }

    data := bytes.NewBuffer([]byte{jsonReq})
    req, err := http.NewRequest("POST", "/File", data)
    req.Header = headers

    client := &http.Client{}
    resp, err := client.Do(req)
    // ...
}

```

`POST /File`

*Allows users to upload files specified in form-data/files*

> Body parameter

```yaml
files:
  - string

```

<h3 id="post__file-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|object|false|none|
|� files|body|[string]|false|none|

> Example responses

> 400 Response

```
"string"
```

```json
"string"
```

<h3 id="post__file-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|string|
|401|[Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1)|Unauthorized|[ProblemDetails](#schemaproblemdetails)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Server Error|string|

<aside class="success">
This operation does not require authentication
</aside>

## get__File

> Code samples

```shell
# You can also use wget
curl -X GET /File \
  -H 'Accept: text/plain'

```

```http
GET /File HTTP/1.1

Accept: text/plain

```

```javascript

const headers = {
  'Accept':'text/plain'
};

fetch('/File',
{
  method: 'GET',

  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

```ruby
require 'rest-client'
require 'json'

headers = {
  'Accept' => 'text/plain'
}

result = RestClient.get '/File',
  params: {
  }, headers: headers

p JSON.parse(result)

```

```python
import requests
headers = {
  'Accept': 'text/plain'
}

r = requests.get('/File', headers = headers)

print(r.json())

```

```php
<?php

require 'vendor/autoload.php';

$headers = array(
    'Accept' => 'text/plain',
);

$client = new \GuzzleHttp\Client();

// Define array of request body.
$request_body = array();

try {
    $response = $client->request('GET','/File', array(
        'headers' => $headers,
        'json' => $request_body,
       )
    );
    print_r($response->getBody()->getContents());
 }
 catch (\GuzzleHttp\Exception\BadResponseException $e) {
    // handle exception or api errors.
    print_r($e->getMessage());
 }

 // ...

```

```java
URL obj = new URL("/File");
HttpURLConnection con = (HttpURLConnection) obj.openConnection();
con.setRequestMethod("GET");
int responseCode = con.getResponseCode();
BufferedReader in = new BufferedReader(
    new InputStreamReader(con.getInputStream()));
String inputLine;
StringBuffer response = new StringBuffer();
while ((inputLine = in.readLine()) != null) {
    response.append(inputLine);
}
in.close();
System.out.println(response.toString());

```

```go
package main

import (
       "bytes"
       "net/http"
)

func main() {

    headers := map[string][]string{
        "Accept": []string{"text/plain"},
    }

    data := bytes.NewBuffer([]byte{jsonReq})
    req, err := http.NewRequest("GET", "/File", data)
    req.Header = headers

    client := &http.Client{}
    resp, err := client.Do(req)
    // ...
}

```

`GET /File`

*Returns all of the Files the user has Uploaded*

> Example responses

> 200 Response

```
{"guid":"ee6a7af7-650d-499b-8e32-58a52ffdb7bc","id":0,"md5":"string","name":"string","user":{"name":"string"}}
```

```json
{
  "guid": "ee6a7af7-650d-499b-8e32-58a52ffdb7bc",
  "id": 0,
  "md5": "string",
  "name": "string",
  "user": {
    "name": "string"
  }
}
```

<h3 id="get__file-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[File](#schemafile)|
|401|[Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1)|Unauthorized|string|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="assetmanager-user">User</h1>

## post__User_Login

> Code samples

```shell
# You can also use wget
curl -X POST /User/Login \
  -H 'Content-Type: application/json' \
  -H 'Accept: text/plain'

```

```http
POST /User/Login HTTP/1.1

Content-Type: application/json
Accept: text/plain

```

```javascript
const inputBody = '{
  "name": "string",
  "password": "string"
}';
const headers = {
  'Content-Type':'application/json',
  'Accept':'text/plain'
};

fetch('/User/Login',
{
  method: 'POST',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

```ruby
require 'rest-client'
require 'json'

headers = {
  'Content-Type' => 'application/json',
  'Accept' => 'text/plain'
}

result = RestClient.post '/User/Login',
  params: {
  }, headers: headers

p JSON.parse(result)

```

```python
import requests
headers = {
  'Content-Type': 'application/json',
  'Accept': 'text/plain'
}

r = requests.post('/User/Login', headers = headers)

print(r.json())

```

```php
<?php

require 'vendor/autoload.php';

$headers = array(
    'Content-Type' => 'application/json',
    'Accept' => 'text/plain',
);

$client = new \GuzzleHttp\Client();

// Define array of request body.
$request_body = array();

try {
    $response = $client->request('POST','/User/Login', array(
        'headers' => $headers,
        'json' => $request_body,
       )
    );
    print_r($response->getBody()->getContents());
 }
 catch (\GuzzleHttp\Exception\BadResponseException $e) {
    // handle exception or api errors.
    print_r($e->getMessage());
 }

 // ...

```

```java
URL obj = new URL("/User/Login");
HttpURLConnection con = (HttpURLConnection) obj.openConnection();
con.setRequestMethod("POST");
int responseCode = con.getResponseCode();
BufferedReader in = new BufferedReader(
    new InputStreamReader(con.getInputStream()));
String inputLine;
StringBuffer response = new StringBuffer();
while ((inputLine = in.readLine()) != null) {
    response.append(inputLine);
}
in.close();
System.out.println(response.toString());

```

```go
package main

import (
       "bytes"
       "net/http"
)

func main() {

    headers := map[string][]string{
        "Content-Type": []string{"application/json"},
        "Accept": []string{"text/plain"},
    }

    data := bytes.NewBuffer([]byte{jsonReq})
    req, err := http.NewRequest("POST", "/User/Login", data)
    req.Header = headers

    client := &http.Client{}
    resp, err := client.Do(req)
    // ...
}

```

`POST /User/Login`

*Allows the user to log in*

> Body parameter

```json
{
  "name": "string",
  "password": "string"
}
```

<h3 id="post__user_login-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[AuthenticationParams](#schemaauthenticationparams)|false|The shema of data required for a users login|

> Example responses

> 200 Response

```
"string"
```

```json
"string"
```

<h3 id="post__user_login-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|string|

<aside class="success">
This operation does not require authentication
</aside>

## post__User_Register

> Code samples

```shell
# You can also use wget
curl -X POST /User/Register \
  -H 'Content-Type: application/json'

```

```http
POST /User/Register HTTP/1.1

Content-Type: application/json

```

```javascript
const inputBody = '{
  "name": "string",
  "password": "string"
}';
const headers = {
  'Content-Type':'application/json'
};

fetch('/User/Register',
{
  method: 'POST',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

```ruby
require 'rest-client'
require 'json'

headers = {
  'Content-Type' => 'application/json'
}

result = RestClient.post '/User/Register',
  params: {
  }, headers: headers

p JSON.parse(result)

```

```python
import requests
headers = {
  'Content-Type': 'application/json'
}

r = requests.post('/User/Register', headers = headers)

print(r.json())

```

```php
<?php

require 'vendor/autoload.php';

$headers = array(
    'Content-Type' => 'application/json',
);

$client = new \GuzzleHttp\Client();

// Define array of request body.
$request_body = array();

try {
    $response = $client->request('POST','/User/Register', array(
        'headers' => $headers,
        'json' => $request_body,
       )
    );
    print_r($response->getBody()->getContents());
 }
 catch (\GuzzleHttp\Exception\BadResponseException $e) {
    // handle exception or api errors.
    print_r($e->getMessage());
 }

 // ...

```

```java
URL obj = new URL("/User/Register");
HttpURLConnection con = (HttpURLConnection) obj.openConnection();
con.setRequestMethod("POST");
int responseCode = con.getResponseCode();
BufferedReader in = new BufferedReader(
    new InputStreamReader(con.getInputStream()));
String inputLine;
StringBuffer response = new StringBuffer();
while ((inputLine = in.readLine()) != null) {
    response.append(inputLine);
}
in.close();
System.out.println(response.toString());

```

```go
package main

import (
       "bytes"
       "net/http"
)

func main() {

    headers := map[string][]string{
        "Content-Type": []string{"application/json"},
    }

    data := bytes.NewBuffer([]byte{jsonReq})
    req, err := http.NewRequest("POST", "/User/Register", data)
    req.Header = headers

    client := &http.Client{}
    resp, err := client.Do(req)
    // ...
}

```

`POST /User/Register`

*Allows to create a new user*

> Body parameter

```json
{
  "name": "string",
  "password": "string"
}
```

<h3 id="post__user_register-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[AuthenticationParams](#schemaauthenticationparams)|false|The shema of data required for a users login|

<h3 id="post__user_register-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|None|

<aside class="success">
This operation does not require authentication
</aside>

# Schemas

<h2 id="tocS_AuthenticationParams">AuthenticationParams</h2>
<!-- backwards compatibility -->
<a id="schemaauthenticationparams"></a>
<a id="schema_AuthenticationParams"></a>
<a id="tocSauthenticationparams"></a>
<a id="tocsauthenticationparams"></a>

```json
{
  "name": "string",
  "password": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string�null|false|none|none|
|password|string�null|false|none|none|

<h2 id="tocS_File">File</h2>
<!-- backwards compatibility -->
<a id="schemafile"></a>
<a id="schema_File"></a>
<a id="tocSfile"></a>
<a id="tocsfile"></a>

```json
{
  "guid": "ee6a7af7-650d-499b-8e32-58a52ffdb7bc",
  "id": 0,
  "md5": "string",
  "name": "string",
  "user": {
    "name": "string"
  }
}

```

Describes the file object which is stored inside the database

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|guid|string(uuid)|true|none|The guid the document is to be referenced by|
|id|integer(int64)|false|none|The primary database Id<br>Do not touch :)|
|md5|string|true|none|The computed md5 checksum|
|name|string|true|none|The name of the file|
|user|[User](#schemauser)|true|none|The database user object|

<h2 id="tocS_FileStream">FileStream</h2>
<!-- backwards compatibility -->
<a id="schemafilestream"></a>
<a id="schema_FileStream"></a>
<a id="tocSfilestream"></a>
<a id="tocsfilestream"></a>

```json
{
  "canTimeout": true,
  "readTimeout": 0,
  "writeTimeout": 0,
  "handle": {},
  "canRead": true,
  "canWrite": true,
  "safeFileHandle": {
    "isClosed": true,
    "isInvalid": true,
    "isAsync": true
  },
  "name": "string",
  "isAsync": true,
  "length": 0,
  "position": 0,
  "canSeek": true
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|canTimeout|boolean|false|read-only|none|
|readTimeout|integer(int32)|false|none|none|
|writeTimeout|integer(int32)|false|none|none|
|handle|[IntPtr](#schemaintptr)|false|none|none|
|canRead|boolean|false|read-only|none|
|canWrite|boolean|false|read-only|none|
|safeFileHandle|[SafeFileHandle](#schemasafefilehandle)|false|none|none|
|name|string�null|false|read-only|none|
|isAsync|boolean|false|read-only|none|
|length|integer(int64)|false|read-only|none|
|position|integer(int64)|false|none|none|
|canSeek|boolean|false|read-only|none|

<h2 id="tocS_IntPtr">IntPtr</h2>
<!-- backwards compatibility -->
<a id="schemaintptr"></a>
<a id="schema_IntPtr"></a>
<a id="tocSintptr"></a>
<a id="tocsintptr"></a>

```json
{}

```

### Properties

*None*

<h2 id="tocS_ProblemDetails">ProblemDetails</h2>
<!-- backwards compatibility -->
<a id="schemaproblemdetails"></a>
<a id="schema_ProblemDetails"></a>
<a id="tocSproblemdetails"></a>
<a id="tocsproblemdetails"></a>

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string",
  "property1": null,
  "property2": null
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|**additionalProperties**|any|false|none|none|
|type|string�null|false|none|none|
|title|string�null|false|none|none|
|status|integer(int32)�null|false|none|none|
|detail|string�null|false|none|none|
|instance|string�null|false|none|none|

<h2 id="tocS_SafeFileHandle">SafeFileHandle</h2>
<!-- backwards compatibility -->
<a id="schemasafefilehandle"></a>
<a id="schema_SafeFileHandle"></a>
<a id="tocSsafefilehandle"></a>
<a id="tocssafefilehandle"></a>

```json
{
  "isClosed": true,
  "isInvalid": true,
  "isAsync": true
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|isClosed|boolean|false|read-only|none|
|isInvalid|boolean|false|read-only|none|
|isAsync|boolean|false|read-only|none|

<h2 id="tocS_User">User</h2>
<!-- backwards compatibility -->
<a id="schemauser"></a>
<a id="schema_User"></a>
<a id="tocSuser"></a>
<a id="tocsuser"></a>

```json
{
  "name": "string"
}

```

The database user object

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|true|none|The username|

