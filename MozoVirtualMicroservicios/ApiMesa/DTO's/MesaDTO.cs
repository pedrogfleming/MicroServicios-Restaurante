namespace ApiMesa.DTO_s
{
    public class MesaDTO
    {
        public int? Id { get; set; }
        public string Codigo { get; set; }
        public int QuantityOfPerson { get; set; }
        public bool IsAvailable { get; set; }
        public int WaiterId { get; set; }
    }
}
