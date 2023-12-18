using Blazored.LocalStorage;
using StandAloneWasmTesting.Models;

namespace StandAloneWasmTesting.Services;

public class ProductRepository(ILocalStorageService localStorage)
{
    private readonly ILocalStorageService _localStorage = localStorage;

    /// <summary>
    /// Localstorage key used for storing all the products in the browsers local storage
    /// </summary>
    private const string _productLocalStorageKey = "Products";

    /// <summary>
    /// In-memory cache of the products
    /// </summary>
    private List<Product>? _products;

    /// <summary>
    /// Fetched the products from local storage if not done before
    /// </summary>
    /// <returns></returns>
    private async Task EnsureProductsAreLoaded()
    {
        if (_products is not null)
        {
            return;
        }

        if (await _localStorage.ContainKeyAsync(_productLocalStorageKey) is false)
        {
            _products = [];
            return;
        }

        _products = await _localStorage.GetItemAsync<List<Product>>(_productLocalStorageKey);
    }

    /// <summary>
    /// Saved the in-memory list of products to the local storage
    /// </summary>
    /// <returns></returns>
    private async Task SaveListToLocalStorage()
    {
        await _localStorage.SetItemAsync(_productLocalStorageKey, _products);
    }

    /// <summary>
    /// Gets and returns all the products.
    /// </summary>
    /// <returns></returns>
    public async Task<List<Product>> GetAllProductsAsync()
    {
        await EnsureProductsAreLoaded();
        return _products!;
    }

    /// <summary>
    /// Gets a single product by its ID
    /// </summary>
    /// <param name="id">The id of the product</param>
    /// <returns>The product if it exists. null if it does not</returns>
    public async Task<Product?> GetSingleProduct(int id)
    {
        await EnsureProductsAreLoaded();
        return _products!.FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    /// Saves the changes to the product
    /// </summary>
    /// <param name="product">The target product</param>
    /// <returns></returns>
    public async Task UpdateProduct(Product product)
    {
        await EnsureProductsAreLoaded();
        int index = _products!.FindIndex(p => p.Id == product.Id);

        if (index == -1) { return; }

        _products[index] = product;

        await SaveListToLocalStorage();
    }

    /// <summary>
    /// Deletes the product.
    /// </summary>
    /// <param name="product">The target product to be deleted</param>
    /// <returns></returns>
    public async Task DeleteProduct(Product product)
    {
        await EnsureProductsAreLoaded();
        _products!.Remove(_products.FirstOrDefault(p => p.Id == product.Id));
        await SaveListToLocalStorage();
    }
}
