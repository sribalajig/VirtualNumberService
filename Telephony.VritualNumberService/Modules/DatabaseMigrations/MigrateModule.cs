using System.Data.Entity;
using System.Linq;
using Nancy;
using Telephony.VritualNumberService.Data.Persistence;
//using Telephony.VritualNumberService.Migrations;

namespace Telephony.VritualNumberService.Modules.DatabaseMigrations
{
    public class MigrateModule : NancyModule
    {
        public MigrateModule()
        {
            Post["/Database/Migrate"] = _ =>
            {
                //Database.SetInitializer(
                //    new MigrateDatabaseToLatestVersion
                //        <VirtualNumberContext, Configuration>());

                using (var databaseContext = new VirtualNumberContext())
                {
                    var databaseStatus = databaseContext.Purposes.Any();

                    if (databaseStatus)
                        return Response.AsText("Migration successful.");

                    return Response.AsText("Something went wrong with the migrations.");
                }
            };
        }
    }
}