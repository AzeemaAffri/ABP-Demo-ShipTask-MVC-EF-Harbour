using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Harbour.Ships;

public class Ship : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public string Type  { get; set; }
    public int YearBuilt { get; set; }

    public int PassengerCapacity { get; set; }

    private Ship()
    {
        /* This constructor is for deserialization / ORM purpose */
    }

    internal Ship(
        Guid id,
        string name,
        string type,
        int yearBuilt,
        int passengerCapacity)
        : base(id)
    {
        Name = name;
        Type = type;
        YearBuilt = yearBuilt;
        PassengerCapacity = passengerCapacity;

    }

    internal Ship ChangeName(string name)
    {
        SetName(name);
        return this;
    }

    private void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: ShipConsts.MaxNameLength
        );
    }
}
