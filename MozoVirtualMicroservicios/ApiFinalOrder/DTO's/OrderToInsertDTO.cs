namespace ApiFinalOrder.DTO_s
{
    public class OrderToInsertDTO
    {
        public int? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal ServiceFee { get; set; }
        public int Status { get; set; }
        public int TableNumber { get; set; }
        public int WaiterId { get; set; }
        public int Customers { get; set; }
        public List<ProductIdQtyToInsert> Products { get; set; }

    }
}
