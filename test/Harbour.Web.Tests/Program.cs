using Microsoft.AspNetCore.Builder;
using Harbour;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<HarbourWebTestModule>();

public partial class Program
{
}
