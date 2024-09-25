namespace Enigma_Protocol.Models
{
    public class Product
    {
        // Parameterless constructor
        public Product()
        {
            OrderDetails = []; // Initialize the collection
            Inventories = [];
        }
        // Constructor with parameters
        public Product(int productId, string name, string description, double price, string productType, Inventory inventory)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;
            ProductType = productType;
            Inventory = inventory;
            OrderDetails = []; // Initialize the collection
            Inventories = [];
        }
        // Properties
        public int ProductId { get; set; }  // Primary Key
        public string Name { get; set; }  // Nome del prodotto (es. "Stanza Escape Room" o "Accessorio")
        public string Description { get; set; }  // Descrizione del prodotto
        public double Price { get; set; }  // Prezzo del prodotto
        public string ProductType { get; set; }  // Tipo di prodotto (es. "Room", "Item", etc.)

        // Relazione uno-a-uno con Inventory
        public Inventory Inventory { get; set; }  // Proprietà di navigazione a Inventory

        // Collection for order details related to the product
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }  // Navigation property to OrderDetails
        public virtual ICollection<Inventory> Inventories { get; set; }  // Navigation property to OrderDetails



    }
}
