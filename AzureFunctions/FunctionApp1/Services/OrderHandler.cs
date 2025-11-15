using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Services
{
    public class OrderHandler : IRequestHandler<ShowOrderCommand, string>
    {
        public Task<string> Handle(ShowOrderCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(string.Format("Order is of id {0}", request.ID));
        }
    }
}
