using AioCore.Domain.Aggregates.DeviceAggregate;
using AioCore.Shared.Common.Constants;
using AioCore.Shared.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AioCore.Application.Commands.DeviceCommands;

public class UpdateDeviceCommand : Device, IRequest<Response<bool>>
{
    internal class Handler : IRequestHandler<UpdateDeviceCommand, Response<bool>>
    {
        private readonly SettingsContext _context;

        public Handler(SettingsContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(
                x => x.Id.Equals(request.Id), cancellationToken);
            if (device is null)
                return new Response<bool>
                {
                    Data = false,
                    Success = false,
                    Message = Messages.DataNotFound
                };
            device.Update(request.DisplayName);
            await _context.SaveChangesAsync(cancellationToken);
            return new Response<bool>
            {
                Data = true,
                Success = true,
                Message = Messages.UpdateDataSuccessful
            };
        }
    }
}