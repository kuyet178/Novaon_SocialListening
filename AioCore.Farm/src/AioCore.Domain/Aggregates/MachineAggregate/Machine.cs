using AioCore.Shared.ModelCores;

namespace AioCore.Domain.Aggregates.MachineAggregate
{
    public class Machine : Entity
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime ModifiedAt { get; set; } = DateTime.Now;

        public string TimestampShort { get; set; } = default!;
    }
}