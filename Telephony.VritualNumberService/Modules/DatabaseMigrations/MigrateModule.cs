using Nancy;

namespace Telephony.VritualNumberService.Modules.DatabaseMigrations
{
    public class MigrateModule : NancyModule
    {
        public MigrateModule()
        {
            Post["/Database/Migrate"] = _ => 
                Response.AsJson(HttpStatusCode.Created);
        }
    }
}