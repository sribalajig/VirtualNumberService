using System.Data.Entity.Migrations;

using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Entities;
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

                // Add babajob user types
                database.BabajobUserTypes.Add(new BabajobUserType
                {
                    UserTypeId = 1,
                    UserType = "Employer"
                });

                database.BabajobUserTypes.Add(new BabajobUserType
                {
                    UserTypeId = 2,
                    UserType = "JobSeeker"
                });

                // Add exotel as a default provider
                database.Providers.Add(new Provider(1, "Exotel"));

                database.SaveChanges();
            }
        }
        
        public override void Down()
        {
        }
    }
}
