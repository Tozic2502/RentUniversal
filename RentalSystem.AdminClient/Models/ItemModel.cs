namespace RentalSystem.AdminClient.Models
{
    
    /// <summary>
    /// Represents a item entry retrieved from the API.
    /// Used in the admin panel to display item information
    /// </summary>
    public class ItemModel
    
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public string Condition { get; set; } = "";
        public decimal Value { get; set; }
        public bool IsAvailable { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        
    }
}