using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AioCore.APIs.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SocialAccountController : BaseController
{
    public SocialAccountController(IMediator mediator) : base(mediator)
    {
    }
}