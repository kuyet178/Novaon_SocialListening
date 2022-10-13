using AioCore.Shared.Common.Constants;
using AioCore.Shared.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AioCore.Application.Queries.MachineQueries;

public class ListMachineQuery : IRequest<Response<List<Machine>>>
{
    internal class Handler : IRequestHandler<ListMachineQuery, Response<List<Machine>>>
    {
        private readonly SettingsContext _context;

        public Handler(SettingsContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Machine>>> Handle(ListMachineQuery request, CancellationToken cancellationToken)
        {
            var machines = await _context.Machines.ToListAsync(cancellationToken);
            if (machines.Any()) return new Response<List<Machine>>
            {
                Data = machines,
                Success = true
            };
            return new Response<List<Machine>>
            {
                Message = Messages.DataNotFound,
                Success = false
            };
        }
    }
}