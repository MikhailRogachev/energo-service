# Energo Service DEMO


### Overview
This demo shows simple communication between microservices and a management application.

### Context diagram

```mermaid
erDiagram
    direction LR
    
    CUSTOMER ||--o{ ACCOUNT: contains
    ACCOUNT ||--|| PRODUCT: use
    CUSTOMER {
        Guid Id
        string Name
        string Description
        CustomerStatus Status
        DateTime CreatedAt
        DateTime LastUpdatedAt
    }
    ACCOUNT {
        Guid Id
        Guid CustomerId
        Guid ProductId
        AccountType AccountType
        DateTime EffectiveDate
        DateTime ValidUntil
        DateTime CreatedAt
        DateTime LastUpdatedAt
    }
    PRODUCT {
         Guid Id
        string Name
        string Description
        ProductStatus Status
        DateTime CreatedAt
        DateTime LastUpdatedAt
    }
```
### Class Diagram

```mermaid
classDiagram
   direction LR

    class BaseModel
    BaseModel: +uuid Id
    BaseModel: +datetime CreatedAt
    BaseModel: +datetime LastUpdatedAt
    BaseModel: +string Description

    class Customer
    Customer: +string Name
    Customer: +CustomerStatus Status
    Customer: +IReadOnlyCollection~Account~ Accounts  

    class CustomerStatus {
      <<enum>>
      Inactive
      Active
      Deleted
    }  

    class Account
    Account: +uuid? CustomerId
    Account: +uuid? ProductId
    Account: +AccountType AccountType
    Account: +datetime EffectiveDate
    Account: +datetime ValidUntil

    class AccountType {
      <<enum>>
      Na
      Business
      Personal
    }

    class Product
    Product: +string Name
    Product: +ProductStatus Status

    class ProductStatus {
      <<enum>>
      Inactive
      Active
      Deleted
    }

    BaseModel <|-- Customer
    BaseModel <|-- Account
    BaseModel <|-- Product
    Customer -- CustomerStatus
    Account -- AccountType
    Product -- ProductStatus
```







### Add Customer
The diagram below shows the "Add customer Process".

```mermaid
sequenceDiagram
    actor User
    participant customer.api
    participant Broker
    participant management.api
    participant Database

    User ->> customer.api: Request
    customer.api ->> Broker: Produce Message
    customer.api ->> User: Response
    Broker <<->> management.api: Consume Message
    management.api ->> Database: Save Customer
```
