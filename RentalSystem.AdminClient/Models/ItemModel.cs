namespace RentalSystem.AdminClient.Models
{
    public class ItemModel
    {
        public string Id { get; set; } = "";

        public string Name { get; set; } = "";

        public string Category { get; set; } = "";

        public string Condition { get; set; } = "";

        public decimal Value { get; set; }

        public bool IsAvailable { get; set; }
    }
}