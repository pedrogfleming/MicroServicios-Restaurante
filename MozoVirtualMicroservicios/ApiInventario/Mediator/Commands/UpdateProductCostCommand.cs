using MediatR;

namespace ApiInventario.Commands
{
    public class UpdateProductCostCommand : IRequest<bool>
    {
        public int Category { get; set; }
        public decimal Cost { get; set; }
        public UpdateProductCostCommand(int category, decimal cost)
        {
            Category = category;
            Cost = cost;
        }
    }
}
