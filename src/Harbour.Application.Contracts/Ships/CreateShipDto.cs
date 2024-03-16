using System;
using System.ComponentModel.DataAnnotations;

namespace Harbour.Ships;

public class CreateShipDto
{
    [Required]
    [StringLength(ShipConsts.MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string  Type { get; set; }

    public int YearBuilt { get; set; }

    public int PassengerCapacity { get; set; }
}
