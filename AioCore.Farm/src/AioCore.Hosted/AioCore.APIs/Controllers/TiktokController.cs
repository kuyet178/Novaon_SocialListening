using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AioCore.Services.AutomationServices;
using AioCore.Services.AutomationServices.TiktokScenarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AioCore.APIs.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class TiktokController : BaseController
{
    private readonly ScenarioBuilder.TiktokScenarioBuilder _tiktokService;

    public TiktokController(IMediator mediator,
        ScenarioBuilder.TiktokScenarioBuilder tiktokService) : base(mediator)
    {
        _tiktokService = tiktokService;
    }

    [HttpGet]
    public async Task<IActionResult> Execute()
    {
        var tiktokAccount = new TiktokAccount
        {
            DeviceConnection = "emulator-5554",
            DevicePlatform = "Android",
            DeviceVersion = "9",
            AppiumPort = 4723,
            SystemPort = 8200
     
        };
        await _tiktokService.StartAsync(tiktokAccount, new List<StepType>
        {
            StepType.ClearData,
            StepType.OpenAPK,
            StepType.OpenProfile,
            StepType.SurfingNewsFeed
        });
        return Ok();
    }
}
