namespace Telephony.VritualNumberService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompositeKeyVNumberAssociation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.VirtualNumberAssociations", "IX_Unique_Callee_Caller_VirtualNumber_Job");
            DropPrimaryKey("dbo.VirtualNumberAssociations");
            AddPrimaryKey("dbo.VirtualNumberAssociations", new[] { "CallerId", "CalleeId", "VirtualNumberId", "BabajobJobId" });
            CreateIndex("dbo.VirtualNumberAssociations", "CallerId");
            CreateIndex("dbo.VirtualNumberAssociations", "CalleeId");
            CreateIndex("dbo.VirtualNumberAssociations", "VirtualNumberId");
            DropColumn("dbo.VirtualNumberAssociations", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VirtualNumberAssociations", "Id", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.VirtualNumberAssociations", new[] { "VirtualNumberId" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "CalleeId" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "CallerId" });
            DropPrimaryKey("dbo.VirtualNumberAssociations");
            AddPrimaryKey("dbo.VirtualNumberAssociations", "Id");
            CreateIndex("dbo.VirtualNumberAssociations", new[] { "CallerId", "CalleeId", "VirtualNumberId", "BabajobJobId" }, unique: true, name: "IX_Unique_Callee_Caller_VirtualNumber_Job");
        }
    }
}
