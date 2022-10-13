using AioCore.Application.Commands.DeviceCommands;
using AioCore.Application.Execute;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AioCore.APIs.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DeviceController : BaseController
{
    public DeviceController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<string> Ping([FromBody] PingExecute execute)
    {
        return await Mediator.Send(execute);
    }

    [HttpPost]
    public async Task<IActionResult> Import([FromBody] ImportDeviceCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}