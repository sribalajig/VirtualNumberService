namespace Telephony.VritualNumberService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueConstraintsVNumberAssociation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.VirtualNumberAssociations", new[] { "CallerId" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "CalleeId" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "VirtualNumberId" });
            CreateIndex("dbo.VirtualNumberAssociations", new[] { "CallerId", "CalleeId", "VirtualNumberId", "BabajobJobId" }, unique: true, name: "IX_Unique_Callee_Caller_VirtualNumber_Job");
        }
        
        public override void Down()
        {
            DropIndex("dbo.VirtualNumberAssociations", "IX_Unique_Callee_Caller_VirtualNumber_Job");
            CreateIndex("dbo.VirtualNumberAssociations", "VirtualNumberId");
            CreateIndex("dbo.VirtualNumberAssociations", "CalleeId");
            CreateIndex("dbo.VirtualNumberAssociations", "CallerId");
        }
    }
}
