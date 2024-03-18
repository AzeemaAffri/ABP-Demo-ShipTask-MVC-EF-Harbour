using System;
using Volo.Abp.Application.Dtos;

namespace Harbour.Fleets;

public class FleetDto : AuditedEntityDto<Guid>
{
    public Guid ShipId { get; set; }
    public string ShipName { get; set; }
      
    public string Type { get; set; }

    public int YearBuilt { get; set; }
    public int PassengerCapacity { get; set; }

   public string Name { get; set; }

    public HarbourShips HarbourShip { get; set; }

  
}
