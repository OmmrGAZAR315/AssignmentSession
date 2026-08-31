namespace Advanced;

class Program
{
    static void Main(string[] args)
    {
        List<Product> catalog = new()
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200, Stock = 10 },
            new Product { Id = 2, Name = "Phone", Category = "Electronics", Price = 800, Stock = 25 },
            new Product { Id = 3, Name = "T-Shirt", Category = "Clothing", Price = 30, Stock = 100 },
            new Product { Id = 4, Name = "Jeans", Category = "Clothing", Price = 60, Stock = 50 },
            new Product { Id = 5, Name = "Chocolate", Category = "Food", Price = 5, Stock = 200 },
            new Product { Id = 6, Name = "Coffee Beans", Category = "Food", Price = 15, Stock = 80 },
            new Product { Id = 7, Name = "C# Book", Category = "Books", Price = 45, Stock = 30 },
            new Product { Id = 8, Name = "Novel", Category = "Books", Price = 20, Stock = 60 },
            new Product { Id = 9, Name = "Headphones", Category = "Electronics", Price = 150, Stock = 40 },
            new Product { Id = 10, Name = "Jacket", Category = "Clothing", Price = 120, Stock = 15 }
        };

        Console.WriteLine("----Electronics-------");
        var electronics = SearchProducts(catalog,
            (Product x) => { return x.Category == "Electronics"; });
        DisplayProducts(electronics);

        Console.WriteLine("----Under $50-------");
        var under50 = SearchProducts(catalog, p => p.Price < 50);
        DisplayProducts(under50);

        Console.WriteLine("----In Stock-------");
        var inStock = SearchProducts(catalog, p => p.Stock > 0);
        DisplayProducts(inStock);
        Console.WriteLine("----Clothing-------");
        var clothing = SearchProducts(catalog, p => p is { Category: "Clothing", Price: < 100 });
        DisplayProducts(clothing);


        Console.WriteLine("----Short Report-----");
        var shortReport = PrintReport(catalog);
        DisplayProducts(shortReport);

        Console.WriteLine("----Detailed Report-----");
        var detailedReport = PrintReport(catalog);
        DisplayProductsDetailedReport(detailedReport);

        Console.WriteLine("--------Summary List---------");
        var summaryList = TransformProducts(catalog);
        DisplayProductsShortReport(summaryList);

        Console.WriteLine("--------Price Label--------");
        var priceLabel = TransformProducts(catalog, p => p.Price > 100);
        DisplayProductsLabel(priceLabel);

        Console.WriteLine("--------Low Stock--------");
        var lowStock = FilterProducts(catalog, p => p.Stock < 20);
        DisplayProductsLowStock(lowStock);
    }

    private static void DisplayProducts(List<Product> inStock)
    {
        foreach (var product in inStock)
            Console.WriteLine($"- {product.Name}: ${product.Price} (Stock: {product.Stock})");
        Console.WriteLine();
    }

    private static void DisplayProductsShortReport(List<Product> inStock)
    {
        foreach (var product in inStock)
            Console.WriteLine($"- {product.Name}: ${product.Price})");
        Console.WriteLine();
    }

    private static void DisplayProductsLabel(List<Product> inStock)
    {
        foreach (var product in inStock)
            Console.WriteLine($"{product.Category}");
        Console.WriteLine();
    }
    private static void DisplayProductsLowStock(List<Product> inStock)
    {
        foreach (var product in inStock)
            Console.WriteLine($"[LOW STOCK]{product.Name}: only 10 left!");
        Console.WriteLine();
    }

    private static void DisplayProductsDetailedReport(List<Product> inStock)
    {
        foreach (var product in inStock)
            Console.WriteLine(
                $"- [{product.Category}]{product.Name} | Price: ${product.Price} | Stock: {product.Stock}");
        Console.WriteLine();
    }

    private static List<Product> SearchProducts(List<Product> products, Func<Product, bool> delegateProduct)
    {
        List<Product> filteredProducts = [];
        foreach (var product in products)
        {
            if (delegateProduct(product)) filteredProducts.Add(product);
        }

        return filteredProducts;
    }

    private static List<Product> FilterProducts(List<Product> products, Predicate<Product> delegateProduct)
    {
        return products.FindAll(delegateProduct);
    }

    private static List<Product> PrintReport(List<Product> products, Func<Product, bool>? delegateProduct = default)
    {
        List<Product> filteredProducts = [];
        if (delegateProduct == null) return products;
        foreach (var product in products)
        {
            if (delegateProduct(product)) filteredProducts.Add(product);
        }

        return filteredProducts;
    }

    private static List<Product> TransformProducts(List<Product> products,
        Func<Product, bool>? delegateProduct = default)
    {
        List<Product> filteredProducts = [];
        if (delegateProduct == null) return products;
        foreach (var product in products)
        {
            var isProductBiggerThan100 = product.Price > 100;
            var label = isProductBiggerThan100 ? "Expensive" : "Affordable";
            var clonedProduct = product.Clone();
            clonedProduct.Category = $"{product.Name}: {label}{(isProductBiggerThan100 ? "!" : "")}";
            filteredProducts.Add(clonedProduct);
        }

        return filteredProducts;
    }
}