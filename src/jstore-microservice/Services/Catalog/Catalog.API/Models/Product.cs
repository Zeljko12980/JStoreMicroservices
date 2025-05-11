namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public List<string> ImageFile { get; set; } = new();
        public decimal Price { get; set; }
        public int StockQuantity { get; set; } // Opcionalno, ako želiš da pratiš količinu na skladištu
        public bool IsFeatured { get; set; } // Opcionalno, ako želiš da označiš proizvode kao istaknute
        public string Brand { get; set; } = default!; // Opcionalno, ako želiš da pratiš brend proizvoda
        public DateTime CreatedTime { get; set; } // Datum kada je proizvod kreiran
        public DateTime UpdateTime { get; set; } // Datum kada je proizvod poslednji put ažuriran
        public double Weight { get; set; } // Opcionalno, ako želiš da pratiš težinu proizvoda

        // Metod za prikaz proizvoda sa osnovnim informacijama
        public string GetProductSummary()
        {
            return $"{Name} - {Category.FirstOrDefault()} - {Price:C}";
        }

        // Metod za proveru da li je proizvod na skladištu
        public bool IsInStock()
        {
            return StockQuantity > 0;
        }

        // Metod za smanjenje količine proizvoda na skladištu prilikom prodaje
        public void ReduceStock(int quantity)
        {
            if (StockQuantity >= quantity)
            {
                StockQuantity -= quantity;
            }
            else
            {
                throw new InvalidOperationException("Nema dovoljno proizvoda na skladištu.");
            }
        }

        // Metod za povrat proizvoda u skladište
        public void AddStock(int quantity)
        {
            StockQuantity += quantity;
        }
    }
}
