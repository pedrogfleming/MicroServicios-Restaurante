using MediatR;

namespace ApiFinalOrder.Mediator.Queries
{
    public class GetTotalFeeQuery : IRequest<decimal>
    {
        public int WaiterId { get; set; }

        public GetTotalFeeQuery(int waiterId)
        {
            WaiterId = waiterId;
        }
    }
}
