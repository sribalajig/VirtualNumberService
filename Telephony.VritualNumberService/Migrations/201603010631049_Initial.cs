namespace Telephony.VritualNumberService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Purposes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        BabajobUserId = c.Int(nullable: false, identity: true),
                        BabajobUserType = c.String(),
                    })
                .PrimaryKey(t => t.BabajobUserId);
            
            CreateTable(
                "dbo.VirtualNumberAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BabajobJobId = c.Int(nullable: false),
                        Callee_BabajobUserId = c.Int(),
                        Caller_BabajobUserId = c.Int(),
                        State_Id = c.Int(),
                        VirtualNumber_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Callee_BabajobUserId)
                .ForeignKey("dbo.Users", t => t.Caller_BabajobUserId)
                .ForeignKey("dbo.States", t => t.State_Id)
                .ForeignKey("dbo.VirtualNumbers", t => t.VirtualNumber_Id)
                .Index(t => t.Callee_BabajobUserId)
                .Index(t => t.Caller_BabajobUserId)
                .Index(t => t.State_Id)
                .Index(t => t.VirtualNumber_Id);
            
            CreateTable(
                "dbo.VirtualNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Provider_Id = c.Int(),
                        Purpose_Id = c.Int(),
                        VirtualPhoneNumber_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.Provider_Id)
                .ForeignKey("dbo.Purposes", t => t.Purpose_Id)
                .ForeignKey("dbo.PhoneNumbers", t => t.VirtualPhoneNumber_Id)
                .Index(t => t.Provider_Id)
                .Index(t => t.Purpose_Id)
                .Index(t => t.VirtualPhoneNumber_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VirtualNumberAssociations", "VirtualNumber_Id", "dbo.VirtualNumbers");
            DropForeignKey("dbo.VirtualNumbers", "VirtualPhoneNumber_Id", "dbo.PhoneNumbers");
            DropForeignKey("dbo.VirtualNumbers", "Purpose_Id", "dbo.Purposes");
            DropForeignKey("dbo.VirtualNumbers", "Provider_Id", "dbo.Providers");
            DropForeignKey("dbo.VirtualNumberAssociations", "State_Id", "dbo.States");
            DropForeignKey("dbo.VirtualNumberAssociations", "Caller_BabajobUserId", "dbo.Users");
            DropForeignKey("dbo.VirtualNumberAssociations", "Callee_BabajobUserId", "dbo.Users");
            DropIndex("dbo.VirtualNumbers", new[] { "VirtualPhoneNumber_Id" });
            DropIndex("dbo.VirtualNumbers", new[] { "Purpose_Id" });
            DropIndex("dbo.VirtualNumbers", new[] { "Provider_Id" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "VirtualNumber_Id" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "State_Id" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "Caller_BabajobUserId" });
            DropIndex("dbo.VirtualNumberAssociations", new[] { "Callee_BabajobUserId" });
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
