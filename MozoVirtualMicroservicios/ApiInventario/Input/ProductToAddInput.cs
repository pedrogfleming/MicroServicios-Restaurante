namespace ApiInventario.Input
{
    public class ProductToAddInput
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Stock { get; set; }
        public int Category { get; set; }
        public int Type { get; set; }
        public bool IsAvailable { get; set; }
    }
}
