using MediatR;

namespace FunctionApp1.Services
{
    public class ShowOrderCommand : IRequest<string>
    {
        public string ID { get; set; }
    }
}