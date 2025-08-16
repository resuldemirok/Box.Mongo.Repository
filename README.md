# Box.Mongo.Repository

Box.Mongo.Repository is a .NET library that brings Entity Framework Core-like LINQ querying and Repository pattern support to MongoDB.

With Box.Mongo.Repository, you can:
- Query MongoDB using LINQ syntax
- Use Generic Repository for CRUD operations
- Create Specific Repositories for custom queries
- Work with MongoDB in a strongly-typed, EF Core-style API

---

## Installation

You can install the package via NuGet:

```powershell
dotnet add package Box.Mongo.Repository
```

---

## Example Usage

Below is an example of how to use Box.Mongo.Repository to perform CRUD operations and LINQ queries with MongoDB:

```csharp
using System;
using System.Linq;
using Box.Mongo.Repository.Context;
using Box.Mongo.Repository.Repository;
using MongoTestApp.Models;

class Program
{
    static void Main()
    {
        // 1️⃣ Mongo connection
        var context = new MongoDbContext("mongodb://localhost:27017", "testdb");

        // 2️⃣ Create generic repository
        var repo = new GenericRepository<Product>(context);

        // 3️⃣ Add product
        var product = new Product
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Gaming Mouse",
            Price = 650,
            Category = "Electronics"
        };
        repo.Add(product);
        Console.WriteLine("Product added!");

        // 4️⃣ LINQ sample
        var expensiveProducts = repo
            .Where(p => p.Price > 500)
            .ToList();

        Console.WriteLine("Expensive products:");
        foreach (var p in expensiveProducts)
        {
            Console.WriteLine($"{p.Name} - {p.Price} - {p.Category}");
        }

        // 5️⃣ Update product
        product.Price = 600;
        repo.Update(product);
        Console.WriteLine("Product updated!");

        // 6️⃣ Delete product
        repo.Delete(product.Id);
        Console.WriteLine("Product deleted!");
    }
}
```