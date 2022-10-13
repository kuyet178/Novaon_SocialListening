using AioCore.Application.Commands.DeviceCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AioCore.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MachineController : BaseController
    {
        public MachineController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateDeviceCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}