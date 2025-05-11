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
                    Name = "Dell XPS 13",
                    Category = new List<string> { "Laptop", "Electronics" },
                    Description = "Dell XPS 13 laptop with Intel i7, 16GB RAM, and 512GB SSD.",
                    ImageFile = new List<string> { "dell_xps_13_1.jpg", "dell_xps_13_2.jpg" },
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
                    Category = new List<string> { "Laptop", "Electronics" },
                    Description = "MacBook Pro 16-inch with Apple M1 Pro chip, 32GB RAM, and 1TB SSD.",
                    ImageFile = new List<string> { "macbook_pro_16_1.jpg", "macbook_pro_16_2.jpg" },
                    Price = 2399.00m,
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
                    Name = "Logitech MX Master 3",
                    Category = new List<string> { "Accessories", "Mouse" },
                    Description = "Wireless mouse with ergonomic design, customizable buttons, and fast charging.",
                    ImageFile = new List<string> { "logitech_mx_master_3.jpg" },
                    Price = 99.99m,
                    StockQuantity = 150,
                    IsFeatured = true,
                    Brand = "Logitech",
                    Weight = 0.15, // U kilogramima
                    CreatedTime = DateTime.Now.AddMonths(-2),
                    UpdateTime = DateTime.Now.AddMonths(-1)
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Samsung 27-inch Curved Monitor",
                    Category = new List<string> { "Electronics", "Monitors" },
                    Description = "Samsung 27-inch curved monitor with 144Hz refresh rate and 1ms response time.",
                    ImageFile = new List<string> { "samsung_27_curved_monitor.jpg" },
                    Price = 349.99m,
                    StockQuantity = 20,
                    IsFeatured = false,
                    Brand = "Samsung",
                    Weight = 6.5, // U kilogramima
                    CreatedTime = DateTime.Now.AddMonths(-6),
                    UpdateTime = DateTime.Now.AddMonths(-3)
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Bose QuietComfort 35 II",
                    Category = new List<string> { "Accessories", "Headphones" },
                    Description = "Noise-canceling wireless headphones with Alexa integration.",
                    ImageFile = new List<string> { "bose_quietcomfort_35_ii.jpg" },
                    Price = 299.99m,
                    StockQuantity = 75,
                    IsFeatured = true,
                    Brand = "Bose",
                    Weight = 0.5, // U kilogramima
                    CreatedTime = DateTime.Now.AddMonths(-1),
                    UpdateTime = DateTime.Now
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "NVIDIA GeForce RTX 3080",
                    Category = new List<string> { "Components", "Graphics Cards" },
                    Description = "NVIDIA GeForce RTX 3080 graphics card with 10GB of GDDR6X memory.",
                    ImageFile = new List<string> { "nvidia_rtx_3080.jpg" },
                    Price = 799.00m,
                    StockQuantity = 10,
                    IsFeatured = false,
                    Brand = "NVIDIA",
                    Weight = 1.4, // U kilogramima
                    CreatedTime = DateTime.Now.AddMonths(-2),
                    UpdateTime = DateTime.Now.AddMonths(-1)
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Seagate 2TB External Hard Drive",
                    Category = new List<string> { "Storage", "External Drives" },
                    Description = "Portable external hard drive with 2TB capacity and USB 3.0 support.",
                    ImageFile = new List<string> { "seagate_2tb_external.jpg" },
                    Price = 89.99m,
                    StockQuantity = 120,
                    IsFeatured = false,
                    Brand = "Seagate",
                    Weight = 0.3, // U kilogramima
                    CreatedTime = DateTime.Now.AddMonths(-5),
                    UpdateTime = DateTime.Now.AddMonths(-2)
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Razer BlackWidow V3",
                    Category = new List<string> { "Accessories", "Keyboards" },
                    Description = "Mechanical gaming keyboard with RGB lighting and tactile switches.",
                    ImageFile = new List<string> { "razer_blackwidow_v3.jpg" },
                    Price = 129.99m,
                    StockQuantity = 40,
                    IsFeatured = true,
                    Brand = "Razer",
                    Weight = 1.1, // U kilogramima
                    CreatedTime = DateTime.Now.AddMonths(-3),
                    UpdateTime = DateTime.Now.AddMonths(-1)
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TP-Link AC1750 WiFi Router",
                    Category = new List<string> { "Networking", "Routers" },
                    Description = "WiFi router with dual-band support and 1750Mbps speed.",
                    ImageFile = new List<string> { "tplink_ac1750_router.jpg" },
                    Price = 69.99m,
                    StockQuantity = 180,
                    IsFeatured = false,
                    Brand = "TP-Link",
                    Weight = 0.4, // U kilogramima
                    CreatedTime = DateTime.Now.AddMonths(-6),
                    UpdateTime = DateTime.Now.AddMonths(-2)
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Corsair Vengeance 16GB RAM",
                    Category = new List<string> { "Components", "Memory" },
                    Description = "16GB (2x8GB) DDR4 RAM with 3200MHz speed.",
                    ImageFile = new List<string> { "corsair_vengeance_16gb.jpg" },
                    Price = 79.99m,
                    StockQuantity = 200,
                    IsFeatured = true,
                    Brand = "Corsair",
                    Weight = 0.1, // U kilogramima
                    CreatedTime = DateTime.Now.AddMonths(-4),
                    UpdateTime = DateTime.Now.AddMonths(-2)
                }
            };

}
