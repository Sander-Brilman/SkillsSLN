using PlaygroundStandAloneWasm.Models;

namespace PlaygroundStandAloneWasm.Services;

public class ProductRepository
{
    private static readonly List<Product> _products = new()
    {
        new()
        {
            Id = 1,
            Title = "Pizza",
            Description = "very delicius"
        }
    };

    public async Task<List<Product>> GetAllAsync()
    {
        return _products
            .Select(p => new Product 
            { 
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
            })
            .ToList();
    }

    public async Task<Product> GetSingleAsync(int id)
    {
        var productInList = _products.Find(p => p.Id == id);

        return new Product
        {
            Id = productInList.Id,
            Title = productInList.Title,
            Description = productInList.Description,
        };
    }

    public async Task<Product> CreateAsync(Product product)
    {
        int newId = _products.Select(p => p.Id).Max() + 1;

        var newProduct = new Product
        {
            Id = newId,
            Title = product.Title,
            Description = product.Description,
        };

        _products.Add(newProduct);


        return newProduct;
    }

    public async Task UpdateAsync(Product product)
    {
        var productInList = _products.Find(p => product.Id == p.Id);

        productInList!.Title = product.Title;
        productInList!.Description = product.Description;
    }

    public async Task DeleteAsync(Product product)
    {
        _products.Remove(_products.Find(p => p.Id == product.Id));
    }
}
