using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        // Marten UPSERT will cater for existing records
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
            {
                 new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Apple MacBook Pro 14 Inch Space Grey",
                    Category = new List<string> { "Laptops", "Electronics" },
                    Description = "The MacBook Pro 14 Inch in Space Grey is a powerful and sleek laptop, featuring Apple's M1 Pro chip for exceptional performance and a stunning Retina display.",
                    ImageFile = new List<string> { "https://cdn.dummyjson.com/product-images/laptops/apple-macbook-pro-14-inch-space-grey/1.webp", "https://cdn.dummyjson.com/product-images/laptops/apple-macbook-pro-14-inch-space-grey/2.webp","https://cdn.dummyjson.com/product-images/laptops/apple-macbook-pro-14-inch-space-grey/3.webp" },
                    Price = 1299.99m,
                    StockQuantity = 50,
                    IsFeatured = true,
                    Brand = "Dell",
                    Weight = 1.2, // U kilogramima
                    CreatedTime = DateTime.Now.AddMonths(-3),
                    UpdateTime = DateTime.Now.AddMonths(-1)
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Apple MacBook Pro 16",
                    Category = new List<string> { "Laptops", "Electronics" },
                    Description = "MacBook Pro 16-inch with Apple M1 Pro chip, 32GB RAM, and 1TB SSD.",
                    ImageFile = new List<string> { "macbook_pro_16_1.jpg", "macbook_pro_16_2.jpg" },
                    Price = 1999.99m,
                    StockQuantity = 30,
                    IsFeatured = false,
                    Brand = "Apple",
                    Weight = 2.1, // U kilogramima
                    CreatedTime = DateTime.Now.AddMonths(-4),
                    UpdateTime = DateTime.Now.AddMonths(-2)
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Asus Zenbook Pro Dual Screen Laptop",
                    Category = new List<string> { "Electronics", "Laptops" },
                    Description = "The Asus Zenbook Pro Dual Screen Laptop is a high-performance device with dual screens, providing productivity and versatility for creative professionals.",
                    ImageFile = new List<string> { "https://cdn.dummyjson.com/product-images/laptops/asus-zenbook-pro-dual-screen-laptop/1.webp","https://cdn.dummyjson.com/product-images/laptops/asus-zenbook-pro-dual-screen-laptop/2.webp","https://cdn.dummyjson.com/product-images/laptops/asus-zenbook-pro-dual-screen-laptop/3.webp" },
                    Price = 1799.99m,
                    StockQuantity = 150,
                    IsFeatured = true,
                    Brand = "Logitech",
                    Weight = 1.55, // U kilogramima
                    CreatedTime = DateTime.Now.AddMonths(-2),
                    UpdateTime = DateTime.Now.AddMonths(-1)
                },
               new Product
{
    Id = Guid.NewGuid(), // ili Guid.Parse("...") ako želiš da ga fiksiraš
    Name = "Huawei Matebook X Pro",
    Category = new List<string> { "Laptops", "Electronics" },
    Description = "The Huawei Matebook X Pro is a slim and stylish laptop with a high-resolution touchscreen display, offering a premium experience for users on the go.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/laptops/huawei-matebook-x-pro/1.webp",
        "https://cdn.dummyjson.com/product-images/laptops/huawei-matebook-x-pro/2.webp",
        "https://cdn.dummyjson.com/product-images/laptops/huawei-matebook-x-pro/3.webp"
    },
    Price = 1399.99m,
    StockQuantity = 75,
    IsFeatured = true,
    Brand = "Huawei",
    Weight = 9.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.053Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.053Z")
}
,
             new Product
{
    Id = Guid.NewGuid(), // ili Guid.Parse("...") ako želiš da bude fiksni
    Name = "Lenovo Yoga 920",
    Category = new List<string> { "Laptops","Electronics" },
    Description = "The Lenovo Yoga 920 is a 2-in-1 convertible laptop with a flexible hinge, allowing you to use it as a laptop or tablet, offering versatility and portability.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/laptops/lenovo-yoga-920/1.webp",
        "https://cdn.dummyjson.com/product-images/laptops/lenovo-yoga-920/2.webp",
        "https://cdn.dummyjson.com/product-images/laptops/lenovo-yoga-920/3.webp"
    },
    Price = 1099.99m,
    StockQuantity = 40,
    IsFeatured = true, // Ako želiš, možeš i staviti false
    Brand = "Lenovo",
    Weight = 9.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.053Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.053Z")
}
,
              new Product
{
    Id = Guid.NewGuid(), // ili Guid.Parse("...") ako želiš da bude fiksni
    Name = "New DELL XPS 13 9300 Laptop",
    Category = new List<string> { "laptops" },
    Description = "The New DELL XPS 13 9300 Laptop is a compact and powerful device, featuring a virtually borderless InfinityEdge display and high-end performance for various tasks.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/laptops/new-dell-xps-13-9300-laptop/1.webp",
        "https://cdn.dummyjson.com/product-images/laptops/new-dell-xps-13-9300-laptop/2.webp",
        "https://cdn.dummyjson.com/product-images/laptops/new-dell-xps-13-9300-laptop/3.webp"
    },
    Price = 1499.99m,
    StockQuantity = 74,
    IsFeatured = true, // Ako želiš, možeš i staviti false
    Brand = "Dell",
    Weight = 2.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.053Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.053Z")
}
,
            new Product
{
    Id = Guid.NewGuid(), // ili Guid.Parse("...") ako želiš da bude fiksni
    Name = "iPhone 5s",
    Category = new List<string> { "Smartphones" },
    Description = "The iPhone 5s is a classic smartphone known for its compact design and advanced features during its release. While it's an older model, it still provides a reliable user experience.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-5s/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-5s/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-5s/3.webp"
    },
    Price = 199.99m,
    StockQuantity = 25,
    IsFeatured = true, // Možeš staviti false ako nije preporučeni proizvod
    Brand = "Apple",
    Weight = 2.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z")
}
,
              new Product
{
    Id = Guid.NewGuid(), // ili Guid.Parse("...") ako želiš da bude fiksni
    Name = "iPhone 6",
    Category = new List<string> { "smartphones" },
    Description = "The iPhone 6 is a stylish and capable smartphone with a larger display and improved performance. It introduced new features and design elements, making it a popular choice in its time.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-6/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-6/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-6/3.webp"
    },
    Price = 299.99m,
    StockQuantity = 60,
    IsFeatured = true, // Možeš staviti false ako nije preporučeni proizvod
    Brand = "Apple",
    Weight = 7.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z")
}
,
            new Product
{
    Id = Guid.NewGuid(), // ili Guid.Parse("...") ako želiš da bude fiksni
    Name = "iPhone 13 Pro",
    Category = new List<string> { "smartphones" },
    Description = "The iPhone 13 Pro is a cutting-edge smartphone with a powerful camera system, high-performance chip, and stunning display. It offers advanced features for users who demand top-notch technology.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-13-pro/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-13-pro/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-13-pro/3.webp"
    },
    Price = 1099.99m,
    StockQuantity = 56,
    IsFeatured = true, // Možeš staviti false ako nije preporučeni proizvod
    Brand = "Apple",
    Weight = 8.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z")
}
,
             new Product
{
    Id = Guid.NewGuid(), // ili Guid.Parse("...") ako želiš da bude fiksni
    Name = "iPhone X",
    Category = new List<string> { "smartphones" },
    Description = "The iPhone X is a flagship smartphone featuring a bezel-less OLED display, facial recognition technology (Face ID), and impressive performance. It represents a milestone in iPhone design and innovation.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-x/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-x/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/iphone-x/3.webp"
    },
    Price = 899.99m,
    StockQuantity = 37,
    IsFeatured = true, // Možeš staviti false ako nije preporučeni proizvod
    Brand = "Apple",
    Weight = 1.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z")
},
             new Product
{
    Id = Guid.NewGuid(), // ili Guid.Parse("...") ako želiš da bude fiksni
    Name = "Oppo A57",
    Category = new List<string> { "smartphones" },
    Description = "The Oppo A57 is a mid-range smartphone known for its sleek design and capable features. It offers a balance of performance and affordability, making it a popular choice.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/oppo-a57/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/oppo-a57/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/oppo-a57/3.webp"
    },
    Price = 249.99m,
    StockQuantity = 19,
    IsFeatured = true, // Možeš staviti false ako nije preporučeni proizvod
    Brand = "Oppo",
    Weight = 5.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z")
},
             new Product
{
    Id = Guid.NewGuid(), // ili Guid.Parse("...") ako želiš da bude fiksni
    Name = "Oppo F19 Pro Plus",
    Category = new List<string> { "smartphones" },
    Description = "The Oppo F19 Pro Plus is a feature-rich smartphone with a focus on camera capabilities. It boasts advanced photography features and a powerful performance for a premium user experience.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/oppo-f19-pro-plus/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/oppo-f19-pro-plus/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/oppo-f19-pro-plus/3.webp"
    },
    Price = 399.99m,
    StockQuantity = 78,
    IsFeatured = true, // Možeš staviti false ako nije preporučeni proizvod
    Brand = "Oppo",
    Weight = 6.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z")
}
,
             new Product
{
    Id = Guid.NewGuid(), // or Guid.Parse("...") for a specific GUID
    Name = "Oppo K1",
    Category = new List<string> { "smartphones" },
    Description = "The Oppo K1 series offers a range of smartphones with various features and specifications. Known for their stylish design and reliable performance, the Oppo K1 series caters to diverse user preferences.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/oppo-k1/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/oppo-k1/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/oppo-k1/3.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/oppo-k1/4.webp"
    },
    Price = 299.99m,
    StockQuantity = 55,
    IsFeatured = true, // Set false if not featured
    Brand = "Oppo",
    Weight = 5.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
   
},
             new Product
{
    Id = Guid.NewGuid(), // or Guid.Parse("...") for a specific GUID
    Name = "Realme C35",
    Category = new List<string> { "smartphones" },
    Description = "The Realme C35 is a budget-friendly smartphone with a focus on providing essential features for everyday use. It offers a reliable performance and user-friendly experience.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/realme-c35/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/realme-c35/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/realme-c35/3.webp"
    },
    Price = 149.99m,
    StockQuantity = 48,
    IsFeatured = true, // Set false if not featured
    Brand = "Realme",
    Weight = 2.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),

},
             new Product
{
    Id = Guid.NewGuid(), // or Guid.Parse("...") for a specific GUID
    Name = "Realme X",
    Category = new List<string> { "smartphones" },
    Description = "The Realme X is a mid-range smartphone known for its sleek design and impressive display. It offers a good balance of performance and camera capabilities for users seeking a quality device.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/realme-x/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/realme-x/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/realme-x/3.webp"
    },
    Price = 299.99m,
    StockQuantity = 12,
    IsFeatured = true, // Set false if not featured
    Brand = "Realme",
    Weight = 4.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
 
},
             new Product
{
    Id = Guid.NewGuid(), // or Guid.Parse("...") for a specific GUID
    Name = "Realme XT",
    Category = new List<string> { "smartphones" },
    Description = "The Realme XT is a feature-rich smartphone with a focus on camera technology. It comes equipped with advanced camera sensors, delivering high-quality photos and videos for photography enthusiasts.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/realme-xt/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/realme-xt/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/realme-xt/3.webp"
    },
    Price = 349.99m,
    StockQuantity = 80,
    IsFeatured = true, // Set false if not featured
    Brand = "Realme",
    Weight = 3.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),

},
             new Product
{
    Id = Guid.NewGuid(), // or Guid.Parse("...") for a specific GUID
    Name = "Samsung Galaxy S7",
    Category = new List<string> { "smartphones" },
    Description = "The Samsung Galaxy S7 is a flagship smartphone known for its sleek design and advanced features. It features a high-resolution display, powerful camera, and robust performance.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/samsung-galaxy-s7/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/samsung-galaxy-s7/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/samsung-galaxy-s7/3.webp"
    },
    Price = 299.99m,
    StockQuantity = 67,
    IsFeatured = false, // Set true if featured
    Brand = "Samsung",
    Weight = 10.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),

},
             new Product
{
    Id = Guid.NewGuid(), // or Guid.Parse("...") for a specific GUID
    Name = "Samsung Galaxy S8",
    Category = new List<string> { "smartphones" },
    Description = "The Samsung Galaxy S8 is a premium smartphone with an Infinity Display, offering a stunning visual experience. It boasts advanced camera capabilities and cutting-edge technology.",
    ImageFile = new List<string>
    {
        "https://cdn.dummyjson.com/product-images/smartphones/samsung-galaxy-s8/1.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/samsung-galaxy-s8/2.webp",
        "https://cdn.dummyjson.com/product-images/smartphones/samsung-galaxy-s8/3.webp"
    },
    Price = 499.99m,
    StockQuantity = 0, // Out of Stock
    IsFeatured = false, // Set true if featured
    Brand = "Samsung",
    Weight = 6.0,
    CreatedTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),
    UpdateTime = DateTime.Parse("2025-04-30T09:41:02.054Z"),

}







            };

}
