using FunctionApp1.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly IMediator _mediator;

        public Function1(ILogger<Function1> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Function("Function1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var commandResult = _mediator.Send<string>(new ShowOrderCommand() { ID = "1" }).Result;

            return commandResult != null ?
                new OkObjectResult(commandResult) :
                new BadRequestObjectResult("Bad ID");
        }
    }
}
