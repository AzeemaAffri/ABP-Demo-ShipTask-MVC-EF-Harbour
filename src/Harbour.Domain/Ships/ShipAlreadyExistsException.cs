using Volo.Abp;

namespace Harbour.Ships;

public class ShipAlreadyExistsException : BusinessException
{
    public ShipAlreadyExistsException(string name)
        : base(HarbourDomainErrorCodes.ShipAlreadyExists)
    {
        WithData("name", name);
    }
}
