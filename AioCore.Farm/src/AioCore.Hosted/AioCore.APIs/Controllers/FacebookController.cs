using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AioCore.Services.AutomationServices;
using AioCore.Services.AutomationServices.FacebookScenarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AioCore.APIs.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FacebookController : BaseController
{
    private readonly ScenarioBuilder.FacebookScenarioBuilder _facebookService;

    public FacebookController(IMediator mediator,
        ScenarioBuilder.FacebookScenarioBuilder facebookService) : base(mediator)
    {
        _facebookService = facebookService;
    }

    [HttpGet]
    public async Task<IActionResult> Execute()
    {
        var facebookAccount = new FacebookAccount
        {
            DeviceConnection = "emulator-5554",
            DevicePlatform = "Android",
            DeviceVersion = "9",
            AppiumPort = 4723,
            SystemPort = 8200,
            Uid = "100074537604378",
            Password = "d2A6Fc9IjovYGv6G",
            TwoFactorCode = "YELK YWI2 UPW3 C7UW E2Q3 HJNJ 6YAS 5EBW"
        };
        await _facebookService.StartAsync(facebookAccount, new List<StepType>
        {
            StepType.ClearData,
            StepType.OpenAPK,
            StepType.Login,
        });
        return Ok();
    }
}