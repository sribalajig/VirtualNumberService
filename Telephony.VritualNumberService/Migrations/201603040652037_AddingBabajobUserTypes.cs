namespace Telephony.VritualNumberService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBabajobUserTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BabajobUserTypes",
                c => new
                    {
                        UserTypeId = c.Int(nullable: false, identity: true),
                        UserType = c.String(),
                    })
                .PrimaryKey(t => t.UserTypeId);
            
            AddColumn("dbo.Users", "BabaJobUserTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "BabaJobUserTypeId");
            AddForeignKey("dbo.Users", "BabaJobUserTypeId", "dbo.BabajobUserTypes", "UserTypeId", cascadeDelete: true);
            DropColumn("dbo.Users", "BabajobUserType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "BabajobUserType", c => c.String());
            DropForeignKey("dbo.Users", "BabaJobUserTypeId", "dbo.BabajobUserTypes");
            DropIndex("dbo.Users", new[] { "BabaJobUserTypeId" });
            DropColumn("dbo.Users", "BabaJobUserTypeId");
            DropTable("dbo.BabajobUserTypes");
        }
    }
}
