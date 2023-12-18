using StandAloneWasmTesting.Models;

namespace StandAloneWasmTesting.Services;

public class ProductRepository
{
    /// <summary>
    /// In-memory cache of the products
    /// </summary>
    private readonly List<Product> _products = [
        new Product()
        {
            Title = "Asp.net Licence",
            Description = "Build great websites with C# and blazor!",
            Price = 10,
            Stock = 69,
            ImageUrl = "/favicon.png"
        }
    ];

    /// <summary>
    /// Gets and returns all the products.
    /// </summary>
    /// <returns></returns>
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return _products;
    }

    /// <summary>
    /// Gets a single product by its ID
    /// </summary>
    /// <param name="id">The id of the product</param>
    /// <returns>The product if it exists. null if it does not</returns>
    public async Task<Product?> GetSingleProduct(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    /// Saves the changes to the product
    /// </summary>
    /// <param name="product">The target product</param>
    /// <returns></returns>
    public async Task UpdateProduct(Product product)
    {

    }

    /// <summary>
    /// Deletes the product.
    /// </summary>
    /// <param name="product">The target product to be deleted</param>
    /// <returns></returns>
    public async Task DeleteProduct(Product product)
    {
        _products.Remove(product);
    }
}
