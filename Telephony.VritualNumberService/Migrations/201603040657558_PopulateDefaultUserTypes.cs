using Telephony.VritualNumberService.Data.Persistence;
using Telephony.VritualNumberService.Entities;

namespace Telephony.VritualNumberService.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateDefaultUserTypes : DbMigration
    {
        public override void Up()
        {
            using (var context = new VirtualNumberContext())
            {
                context.BabajobUserTypes.Add(
                    new BabajobUserType { UserTypeId = 1, UserType = "Employer" });

                context.BabajobUserTypes.Add(
                    new BabajobUserType { UserTypeId = 2, UserType = "JobSeeker" });

                context.SaveChanges();
            }
        }

        public override void Down()
        {
        }
    }
}
