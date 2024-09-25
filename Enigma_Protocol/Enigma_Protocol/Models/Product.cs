namespace Enigma_Protocol.Models
{
    public class Product
    {
        public Product(int productId, string name, string description, double price, string productType, Inventory inventory)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;
            ProductType = productType;
            Inventory = inventory;
        }

        public int ProductId { get; set; }  // Primary Key
        public string Name { get; set; }  // Nome del prodotto (es. "Stanza Escape Room" o "Accessorio")
        public string Description { get; set; }  // Descrizione del prodotto
        public double Price { get; set; }  // Prezzo del prodotto
        public string ProductType { get; set; }  // Tipo di prodotto (es. "Room", "Item", etc.)

        // Relazione uno-a-uno con Inventory
        public Inventory Inventory { get; set; }  // Proprietà di navigazione a Inventory
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
