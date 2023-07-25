
# E-Commerce API Project

This project is an e-commerce API built using .NET 6 and backend basics. It leverages interfaces, repositories, Entity Framework, and AutoMapper to provide a robust and scalable solution for managing products, categories, discounts, and inventories.


## Features

- CRUD operations for managing products, categories, discounts, and inventories
- Support for multiple product variants with different options and prices.
- Integration with Entity Framework for database operations and data persistence.
- Use of AutoMapper for mapping between DTOs (Data Transfer Objects) and domain models.
- Interfaces and repositories to provide a decoupled and testable architecture.
- Implementation of backend basics, including routing, controllers, and data validation.
  
## Getting Started

Before running the API, ensure you have the following installed:

- .NET 6 SDK: https://dotnet.microsoft.com/download/dotnet/6.0

1. Clone the repository to your local machine:

```bash 
  git clone https://github.com/your-username/e-commerce-api.git

```

2. Navigate to the project directory:

```bash 
  cd e-commerce-api

```

3. Build the solution:

```bash 
  dotnet build
```

4. Run the API:
```bash 
  dotnet run
```




    
## Api Endpoints
-     GET /api/products: Get a list of all products.

-     GET /api/products/{id}: Get a specific product by ID.

-     POST /api/products: Create a new product.

-     PUT /api/products/{id}: Update an existing product.

-     DELETE /api/products/{id}: Delete a product by ID.

-     GET /api/categories: Get a list of all categories.

-     GET /api/categories/{id}: Get a specific category by ID.

-     POST /api/categories: Create a new category.

-     PUT /api/categories/{id}: Update an existing category.

-     DELETE /api/categories/{id}: Delete a category by ID.

-     GET /api/discounts: Get a list of all discounts.

-     GET /api/discounts/{id}: Get a specific discount by ID.

-     POST /api/discounts: Create a new discount.

-     PUT /api/discounts/{id}: Update an existing discount.

-     DELETE /api/discounts/{id}: Delete a discount by ID.

-     GET /api/inventories: Get a list of all inventories.

-     GET /api/inventories/{id}: Get a specific inventory by ID.

-     POST /api/inventories: Create a new inventory.

-     PUT /api/inventories/{id}: Update an existing inventory.

-     DELETE /api/inventories/{id}: Delete an inventory by ID.