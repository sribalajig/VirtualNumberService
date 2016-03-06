namespace Telephony.VritualNumberService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bootstrap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "VirtualNumbers.BabajobUserTypes",
                c => new
                    {
                        UserTypeId = c.Int(nullable: false),
                        UserType = c.String(),
                    })
                .PrimaryKey(t => t.UserTypeId);
            
            CreateTable(
                "VirtualNumbers.PhoneNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength:15),
                        
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Number, unique: true);
            
            CreateTable(
                "VirtualNumbers.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "VirtualNumbers.Purposes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "VirtualNumbers.States",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "VirtualNumbers.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BabaJobUserTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("VirtualNumbers.BabajobUserTypes", t => t.BabaJobUserTypeId, cascadeDelete: true)
                .Index(t => t.BabaJobUserTypeId);
            
            CreateTable(
                "VirtualNumbers.VirtualNumberAssociations",
                c => new
                    {
                        CallerId = c.Int(nullable: false),
                        CalleeId = c.Int(nullable: false),
                        VirtualNumberId = c.Int(nullable: false),
                        BabajobJobId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CallerId, t.CalleeId, t.VirtualNumberId, t.BabajobJobId })
                .ForeignKey("VirtualNumbers.Users", t => t.CalleeId)
                .ForeignKey("VirtualNumbers.Users", t => t.CallerId)
                .ForeignKey("VirtualNumbers.States", t => t.StateId, cascadeDelete: true)
                .ForeignKey("VirtualNumbers.VirtualNumbers", t => t.VirtualNumberId, cascadeDelete: true)
                .Index(t => t.CallerId)
                .Index(t => t.CalleeId)
                .Index(t => t.VirtualNumberId)
                .Index(t => t.StateId);
            
            CreateTable(
                "VirtualNumbers.VirtualNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberId = c.Int(nullable: false),
                        PurposeId = c.Int(nullable: false),
                        ProviderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("VirtualNumbers.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("VirtualNumbers.Purposes", t => t.PurposeId, cascadeDelete: true)
                .ForeignKey("VirtualNumbers.PhoneNumbers", t => t.NumberId, cascadeDelete: true)
                .Index(t => t.NumberId)
                .Index(t => t.PurposeId)
                .Index(t => t.ProviderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("VirtualNumbers.VirtualNumberAssociations", "VirtualNumberId", "VirtualNumbers.VirtualNumbers");
            DropForeignKey("VirtualNumbers.VirtualNumbers", "NumberId", "VirtualNumbers.PhoneNumbers");
            DropForeignKey("VirtualNumbers.VirtualNumbers", "PurposeId", "VirtualNumbers.Purposes");
            DropForeignKey("VirtualNumbers.VirtualNumbers", "ProviderId", "VirtualNumbers.Providers");
            DropForeignKey("VirtualNumbers.VirtualNumberAssociations", "StateId", "VirtualNumbers.States");
            DropForeignKey("VirtualNumbers.VirtualNumberAssociations", "CallerId", "VirtualNumbers.Users");
            DropForeignKey("VirtualNumbers.VirtualNumberAssociations", "CalleeId", "VirtualNumbers.Users");
            DropForeignKey("VirtualNumbers.Users", "BabaJobUserTypeId", "VirtualNumbers.BabajobUserTypes");
            DropIndex("VirtualNumbers.VirtualNumbers", new[] { "ProviderId" });
            DropIndex("VirtualNumbers.VirtualNumbers", new[] { "PurposeId" });
            DropIndex("VirtualNumbers.VirtualNumbers", new[] { "NumberId" });
            DropIndex("VirtualNumbers.VirtualNumberAssociations", new[] { "StateId" });
            DropIndex("VirtualNumbers.VirtualNumberAssociations", new[] { "VirtualNumberId" });
            DropIndex("VirtualNumbers.VirtualNumberAssociations", new[] { "CalleeId" });
            DropIndex("VirtualNumbers.VirtualNumberAssociations", new[] { "CallerId" });
            DropIndex("VirtualNumbers.Users", new[] { "BabaJobUserTypeId" });
            DropIndex("VirtualNumbers.PhoneNumbers", new[] { "Number" });
            DropTable("VirtualNumbers.VirtualNumbers");
            DropTable("VirtualNumbers.VirtualNumberAssociations");
            DropTable("VirtualNumbers.Users");
            DropTable("VirtualNumbers.States");
            DropTable("VirtualNumbers.Purposes");
            DropTable("VirtualNumbers.Providers");
            DropTable("VirtualNumbers.PhoneNumbers");
            DropTable("VirtualNumbers.BabajobUserTypes");
        }
    }
}
