# shopsservice
 
 Author: Mujdat Karacan
 
## Description
 
ShopsRUs is an existing retail outlet. They would like to provide discount to their customers
on all their web/mobile platforms. They require a set of APIs to be built that provide
capabilities to calculate discounts, generate the total costs and generate the invoices for
customers
The following discounts apply:
1. If the user is an employee of the store, he gets a 30% discount
2. If the user is an affiliate of the store, he gets a 10% discount
3. If the user has been a customer for over 2 years, he gets a 5% discount.
4. For every $100 on the bill, there would be a $ 5 discount (e.g. for $ 990, you get $ 45
as a discount).
5. The percentage based discounts do not apply on groceries.
6. A user can get only one of the percentage based discounts on a bill.
Write a program in a programming language of your choice with test cases such that given a
bill, it finds the net payable amount. Create an RESTful API which when given a bill it returns
the final invoice amount including the discount. Please note the stress is on object oriented
approach and test coverage. User interface is not required.

 
## Clone project 

> git clone https://github.com/mujdatkaracan/shopsservice.git 


## Restore

Restore dependencies and tools for the project in the current directory:

```bash
dotnet restore
```

## Run

Run the ShopsService.WebAPI

```bash
 dotnet run --project https://github.com/mujdatkaracan/shopsservice/ShopsService.WebAPI.csproj
```
 
 
> Launch ShopsService.WebAPI

> Launch https://localhost:44370 on your browser

# Swagger address
> https://localhost:44370/swagger

# Healthcheck UI address
>https://localhost:44370/health-ui

# Healthcheck api address
>https://localhost:44370/health-api




## Using

### API Endpoint

* Http Method - **POST**
* Endpoint - **https://localhost:44370/v1/api/Discounts**

Example request

> user type Employee:0,Affiliate:1,Other:2

>bill item type : Groceries:0,Other:1

```json
{
  "user": {
    "activationDate": "2023-01-23T11:39:07.258Z",
    "type": 0
  },
  "bill": {
    "items": [
      {
        "type": 0,
        "price": 20.5
      },{
        "type": 1,
        "price": 100
      },
{
        "type": 2,
        "price": 590.7
      }

    ]
  }
} 
```

The response is net payable amount.



```json
{"StatusCode":200,"Result":true,"Data":"468.99","ErrorMessage":null,"IsError":false}
```