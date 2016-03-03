namespace Telephony.VritualNumberService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bootstrap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhoneNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Purposes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BabajobUserType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VirtualNumberAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CallerId = c.Int(nullable: false),
                        CalleeId = c.Int(nullable: false),
                        VirtualNumberId = c.Int(nullable: false),
                        BabajobJobId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CalleeId)
                .ForeignKey("dbo.Users", t => t.CallerId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.VirtualNumbers", t => t.VirtualNumberId, cascadeDelete: true)
                .Index(t => t.CallerId)
                .Index(t => t.CalleeId)
                .Index(t => t.VirtualNumberId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.VirtualNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberId = c.Int(nullable: false),
                        PurposeId = c.Int(nullable: false),
                        ProviderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.Purposes", t => t.PurposeId, cascadeDelete: true)
                .ForeignKey("dbo.PhoneNumbers", t => t.NumberId, cascadeDelete: true)
                .Index(t => t.NumberId)
                .Index(t => t.PurposeId)
                .Index(t => t.ProviderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VirtualNumberAssociations", "VirtualNumberId", "dbo.VirtualNumbers");
            DropForeignKey("dbo.VirtualNumbers", "NumberId", "dbo.PhoneNumbers");
            DropForeignKey("dbo.VirtualNumbers", "PurposeId", "dbo.Purposes");
            DropForeignKey("dbo.VirtualNumbers", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.VirtualNumberAssociations", "StateId", "dbo.States");
            DropForeignKey("dbo.VirtualNumberAssociations", "CallerId", "dbo.Users");
            DropForeignKey("dbo.VirtualNumberAssociations", "CalleeId", "dbo.Users");
            DropIndex("dbo.VirtualNumbers", new[] { "ProviderId" });
            DropIndex("dbo.VirtualNumbers", new[] { "PurposeId" });
            DropIndex("dbo.VirtualNumbers", new[] { "NumberId" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "StateId" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "VirtualNumberId" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "CalleeId" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "CallerId" });
            DropTable("dbo.VirtualNumbers");
            DropTable("dbo.VirtualNumberAssociations");
            DropTable("dbo.Users");
            DropTable("dbo.States");
            DropTable("dbo.Purposes");
            DropTable("dbo.Providers");
            DropTable("dbo.PhoneNumbers");
        }
    }
}
