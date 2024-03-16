using System;
using Volo.Abp.Application.Dtos;

namespace Harbour.Fleets;

public class ShipLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
}
