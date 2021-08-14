# Readme

There are some cases that were made easier because of time consumption.

### 1. Use company-specific implementation

There is a requirement that specific clients should have different behavior within same endpoint. To achieve that there are few options:

* Pass custom http header (which is not compliant with HTTP).
* Pass HTTP query/content parameter (which requires additional changes in endpoint).
* (BEST) Assign authorization context to each client and during specific processing you can clearly see for which client you are referring to.
* (Current approach which was easiest for now) Use cookie data to pass what client is.
