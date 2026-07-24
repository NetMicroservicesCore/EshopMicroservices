using Catalog.API.Models.Products.CreateProduct;

namespace Catalog.API.Tests;

public class CreateProductCommandValidatorTests
{
    private readonly CreateProductCommandValidator _validator = new();

    [Fact]
    public void Validate_WithEmptyName_ShouldHaveError()
    {
        var command = new CreateProductCommand("", ["Electronics"], "Desc", "img.jpg", 10);
        var result = _validator.Validate(command);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public void Validate_WithEmptyCategory_ShouldHaveError()
    {
        var command = new CreateProductCommand("Product", [], "Desc", "img.jpg", 10);
        var result = _validator.Validate(command);
        Assert.Contains(result.Errors, e => e.PropertyName == "Category");
    }

    [Fact]
    public void Validate_WithZeroPrice_ShouldHaveError()
    {
        var command = new CreateProductCommand("Product", ["Electronics"], "Desc", "img.jpg", 0);
        var result = _validator.Validate(command);
        Assert.Contains(result.Errors, e => e.PropertyName == "Price");
    }

    [Fact]
    public void Validate_WithValidData_ShouldNotHaveError()
    {
        var command = new CreateProductCommand("Product", ["Electronics"], "Desc", "img.jpg", 10);
        var result = _validator.Validate(command);
        Assert.Empty(result.Errors);
    }
}
