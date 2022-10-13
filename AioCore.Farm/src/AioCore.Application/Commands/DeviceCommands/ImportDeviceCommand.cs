using AioCore.Domain.Aggregates.DeviceAggregate;
using AioCore.Shared.Common.Constants;
using AioCore.Shared.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AioCore.Application.Commands.DeviceCommands;

public class ImportDeviceCommand : IRequest<Response<List<KeyValuePair<string, string>>>>
{
    public string Devices { get; set; } = default!;

    internal class Handler : IRequestHandler<ImportDeviceCommand, Response<List<KeyValuePair<string, string>>>>
    {
        private readonly SettingsContext _context;

        public Handler(SettingsContext context)
        {
            _context = context;
        }

        public async Task<Response<List<KeyValuePair<string, string>>>> Handle(ImportDeviceCommand request,
            CancellationToken cancellationToken)
        {
            var devices = request.Devices.Split('\n').Select(x =>
            {
                var items = x.Split('|');
                if (items.Length.Equals(3))
                {
                    return new Device
                    {
                        Id = items[0],
                        DisplayName = items[1],
                        IPAddress = items[2]
                    };
                }

                return default!;
            }).ToList();
            foreach (var device in devices)
            {
                if (!await _context.Devices.AnyAsync(x => x.Id.Equals(device.Id), cancellationToken: cancellationToken))
                    await _context.Devices.AddAsync(device, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return new Response<List<KeyValuePair<string, string>>>
            {
                Data = devices.Select(x => new KeyValuePair<string, string>(x.Id, x.DisplayName)).ToList(),
                Success = true,
                Message = Messages.ImportDataSuccessful
            };
        }
    }
}