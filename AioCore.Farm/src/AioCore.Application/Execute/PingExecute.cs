using AioCore.Domain.Aggregates.DeviceAggregate;
using AioCore.Shared.Extensions;
using AioCore.Types;
using AntDesign;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AioCore.Application.Execute
{
    public class PingExecute : DeviceCore, IRequest<string>
    {
        internal class Handler : IRequestHandler<PingExecute, string>
        {
            private readonly SettingsContext _context;

            public Handler(SettingsContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(PingExecute request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.DeviceId)) return "Pang";
                var device = await _context.Devices.FirstOrDefaultAsync(x => x.DeviceId.Equals(request.DeviceId),
                    cancellationToken);
                if (device is not null)
                {
                    device.Update(request.IPAddress, request.Gateway, request.Netmask, request.Dns1,
                        request.Dns2, request.Server, request.LeaseDuration, request.Board, request.Device,
                        request.Model, request.Brand, request.Bootloader, request.Display, request.FingerPrint,
                        request.Hardware, request.Host, request.Manufacturer, request.Product, DateTime.Now,
                        request.Type, request.User, request.BuildNumber);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    device = request.To<Device>();
                    await _context.Devices.AddAsync(device, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return await Task.FromResult("Pong");
            }
        }
    }
}