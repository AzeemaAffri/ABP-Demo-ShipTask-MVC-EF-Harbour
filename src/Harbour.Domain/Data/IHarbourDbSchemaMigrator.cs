using System.Threading.Tasks;

namespace Harbour.Data;

public interface IHarbourDbSchemaMigrator
{
    Task MigrateAsync();
}
