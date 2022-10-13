using AioCore.Domain.Aggregates.DeviceAggregate;
using AioCore.Shared.Common.Constants;
using AioCore.Shared.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AioCore.Application.Queries.DeviceQueries;

public class ListDeviceQuery : IRequest<Response<List<Device>>>
{
    internal class Handler : IRequestHandler<ListDeviceQuery, Response<List<Device>>>
    {
        private readonly SettingsContext _context;

        public Handler(SettingsContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Device>>> Handle(ListDeviceQuery request, CancellationToken cancellationToken)
        {
            var devices = await _context.Devices.ToListAsync(cancellationToken);
            if (devices.Any()) return new Response<List<Device>>
            {
                Data = devices,
                Success = true
            };
            return new Response<List<Device>>
            {
                Message = Messages.DataNotFound,
                Success = false
            };
        }
    }
}