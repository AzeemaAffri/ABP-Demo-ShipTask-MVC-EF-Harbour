using System;
using Volo.Abp.Application.Dtos;

namespace Harbour.Fleets;

public class ShipLookupDto : EntityDto<Guid>
{

    public string Name { get; set; }

    public string Type { get; set; }

    public int YearBuilt { get; set; }

    public int PassengerCapacity { get; set; }
}
