using Microsoft.AspNetCore.Http.Json;
using AdvancedDatabaseandORMConcepts.Database;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using AdvancedDatabaseandORMConcepts.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

string connectionString = builder.Configuration.GetConnectionString("ECommerceContextConnection");

builder.Services.AddDbContext<ECommerceDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

//Create the following endpoints:
//	A means of viewing all Customers, with their associated addresses
//	A means of creating a new Product
//	A means of creating a new Order.
//		A customer must make an order to a specific address for a specific product.
//		The API must verify that the customer is associated with the posted address
//	A means of creating an association between a customer and an address
//		The API should verify that the association doesn't already exist

//Instead of seed methods you can start by just inserting some data via Azure 

app.MapGet("/customers", (ECommerceDBContext db) =>
{
    try
    {
        HashSet<Customer> customers = db.Customers.Include(c => c.CustomerAddress).ToHashSet();
        return Results.Ok(customers);
    }
    catch (Exception ex)
    {
        return Results.NotFound(ex.Message);
    }
});

app.MapPost("/products/create-new", (ECommerceDBContext db, string name, string description, int price) =>
{
    try
    {
        if (db.Product.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            return Results.Conflict(); 
        }

        Product newProduct = new Product(name, description, price);

        db.Product.Add(newProduct);

        db.SaveChanges();

        return Results.Created($"/products/{newProduct.Id}", newProduct);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPost("/orders/create-new", (ECommerceDBContext db, int quantity, int productId, int customerId, string addressLine) =>
{
    try
    {
        if (!db.Product.Any(p => p.Id == productId) || !db.CustomersAddresses.Any(ca => ca.CustomerId == customerId && ca.Address.AddressLine1 == addressLine))
        {
            throw new ArgumentNullException();
        }

        Order newOrder = new Order(quantity, db.Product.First(p => p.Id == productId), db.CustomersAddresses.First(ca => ca.CustomerId == customerId && ca.Address.AddressLine1 == addressLine));

        db.Order.Add(newOrder);
        db.Product.First(p => p.Id == productId).Orders.Add(newOrder);
        db.Customers.First(c => c.Id == customerId).Orders.Add(newOrder);

        db.SaveChanges();

        return Results.Ok(newOrder);
    }
    catch (Exception ex)
    {
        return Results.NotFound(ex.Message);
    }
});

app.MapPost("/customers/add-address", (ECommerceDBContext db, int customerId, string addressLine, bool isPrimaryAddress) =>
{
    try
    {
        CustomerAddress newCustomerAddress = new CustomerAddress(db.Customers.First(c => c.Id == customerId), db.Address.First(a => a.AddressLine1 == addressLine), isPrimaryAddress);

        db.CustomersAddresses.Add(newCustomerAddress);
        db.Customers.First(c => c.Id == customerId).CustomerAddress.Add(newCustomerAddress);
        db.Address.First(a => a.AddressLine1 == addressLine).CustomerAddresses.Add(newCustomerAddress);

        db.SaveChanges();

        return Results.Ok(newCustomerAddress);
    }
    catch (Exception ex)
    {
        return Results.NotFound(ex.Message);
    }
});

app.Run();

