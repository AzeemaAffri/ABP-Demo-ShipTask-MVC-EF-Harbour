using System;
using System.ComponentModel.DataAnnotations;

namespace Harbour.Fleets;

public class CreateUpdateFleetDto
{
    public Guid ShipId { get; set; }

    [Required]
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public HarbourShips HarbourShip { get; set; } = HarbourShips.Panamax;

   
}
