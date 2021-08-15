# Readme

There are some cases that were made easier because of time consumption.

### 1. Usage of `partner` cookie value instead of different scenarios.

There is a requirement that specific clients should have different behavior within same endpoint. To achieve that there are few options:

* Pass custom http header (which is not compliant with REST).
* Pass HTTP query/content parameter (which requires additional changes in endpoint).
* (BEST) Assign authorization context to each client and during specific processing you can clearly see for which client you are referring to - JWT token.
* (Current approach which was easiest for now) Use cookie data to pass what client is.

### 2. Usage of `PartnerResolver` instead of different approach.

Because requirements were clear I didn't do the different approach for partner-specific resolving of implementation. Current approach have some limitations such
as:

- Not support for the default implementation (it could be solved easily by introducing `Default` partnerEnum which could be used for such services or make some
  modification for resolver).
- Not support for `IPartnerService` that could be shared in many partners

More time-consuming approach that could be better - named registration of services based on container which is currently registered (each partner assembly could
have its own container and based on that they could be registered with specific `PartnerEnum` key.

### 3. Pagination for `GET /Customers`

Pagination is my fast response for getting a list of customers and be able to support big amount of request at the same time. If we want to make it further
then:

1. Async pattern for API call `202 ACCEPTED -> 200 OK with list`.
2. Memory/distributed cache to limit calls to database (if it is possible to have a bit old data).
3. Improve `Repository` pattern to use no-tracking queries for things that should only goes to return
   data: https://docs.microsoft.com/en-us/ef/core/querying/tracking#no-tracking-queries.

## Scenarios

```
New partner `CompanyC` would like to have different customer validation. He would like to have `FirstName` required only.
```

1. Create new library for such partner. `Api.Test.CompanyC`
2. Add references to `Api.Test.Partner.Interfaces`.
3. Create `CustomerValidator` class in newly created library:
   ```c#
   public class CustomerValidator : AbstractValidator<Customer>, ICustomerValidator
   {
    public CustomerValidator(){
        RuleFor(x=>x.FirstName).NotEmpty();
    }
   }
   ```
4. That's all. It will be automatically registered by `services.AddPartnerServices();` which is being used in `Startup.cs`. 