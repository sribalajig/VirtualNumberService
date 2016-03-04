namespace Telephony.VritualNumberService.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TurnAutoIncrementOffForUserTypes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "BabaJobUserTypeId", "dbo.BabajobUserTypes");
            DropPrimaryKey("dbo.BabajobUserTypes");
            AlterColumn("dbo.BabajobUserTypes", "UserTypeId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.BabajobUserTypes", "UserTypeId");
            AddForeignKey("dbo.Users", "BabaJobUserTypeId", "dbo.BabajobUserTypes", "UserTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "BabaJobUserTypeId", "dbo.BabajobUserTypes");
            DropPrimaryKey("dbo.BabajobUserTypes");
            AlterColumn("dbo.BabajobUserTypes", "UserTypeId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BabajobUserTypes", "UserTypeId");
            AddForeignKey("dbo.Users", "BabaJobUserTypeId", "dbo.BabajobUserTypes", "UserTypeId", cascadeDelete: true);
        }
    }
}
