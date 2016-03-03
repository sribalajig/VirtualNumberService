using System.Data.Entity.Migrations;

using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Entities.Purpose;
using Telephony.VritualNumberService.Entities.States;

namespace Telephony.VritualNumberService.Migrations
{
    public partial class PopulateDefaults : DbMigration
    {
        public override void Up()
        {
            using (var database = new VirtualNumberContext())
            {
                // Add default states
                database.States.Add(new Free());
                database.States.Add(new InUse());
                database.States.Add(new Expired());

                // Add default purposes
                database.Purposes.Add(new FreeJobApplication());
                database.Purposes.Add(new PaidJobApplication());

                database.SaveChanges();
            }
        }
        
        public override void Down()
        {
        }
    }
}
