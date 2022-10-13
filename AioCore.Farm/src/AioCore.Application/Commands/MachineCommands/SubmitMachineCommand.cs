using AioCore.Shared.ValueObjects;
using MediatR;

namespace AioCore.Application.Commands.MachineCommands;

public class SubmitMachineCommand : Machine, IRequest<Response<bool>>
{
    internal class Handler : IRequestHandler<SubmitMachineCommand, Response<bool>>
    {
        public Task<Response<bool>> Handle(SubmitMachineCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}