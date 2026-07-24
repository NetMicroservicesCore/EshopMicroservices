using System.Net;
using System.Net.Http.Json;
using Catalog.API.Models.Products.CreateProduct;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Tests;

public class CreateProductEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CreateProductEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact(Skip = "Requieres PostgreSQL corriendo")]
    public async Task PostProduct_WithValidData_Returns201Created()
    {
        var client = _factory.CreateClient();
        var request = new CreateProductRequest("Test Product", ["Electronics"], "Desc", "img.jpg", 10);

        var response = await client.PostAsJsonAsync("/products", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<CreateProductResponse>();
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task PostProduct_WithEmptyName_Returns400BadRequest()
    {
        var client = _factory.CreateClient();
        var request = new CreateProductRequest("", ["Electronics"], "Desc", "img.jpg", 10);

        var response = await client.PostAsJsonAsync("/products", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);
}
