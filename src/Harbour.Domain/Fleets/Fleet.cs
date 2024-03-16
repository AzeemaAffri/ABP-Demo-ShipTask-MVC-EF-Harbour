using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Harbour.Fleets;

public class Fleet : AuditedAggregateRoot<Guid>
{
    public Guid ShipId { get; set; }

    public string Name { get; set; }

    public HarbourShips HarbourShip { get; set; }


}
