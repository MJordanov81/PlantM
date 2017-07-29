namespace PlantM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcquisitionTypes",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.CustomGroups",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Genus",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Plants",
                c => new
                    {
                        CollectionNumber = c.String(nullable: false, maxLength: 128),
                        LocationName = c.String(nullable: false, maxLength: 128),
                        SpeciesLabelName = c.String(nullable: false, maxLength: 128),
                        Size = c.String(nullable: false),
                        DateOfAcquisition = c.String(nullable: false),
                        DateOfAcquisitionAsDate = c.DateTime(nullable: false),
                        LastRepottingDate = c.DateTime(nullable: false),
                        AcquisitionTypeName = c.String(nullable: false, maxLength: 128),
                        VendorName = c.String(nullable: false, maxLength: 128),
                        AgeAtAcquisition = c.Int(nullable: false),
                        HasWithered = c.Boolean(nullable: false),
                        WitherReason = c.String(),
                        WitheredDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        SoilName = c.String(nullable: false, maxLength: 128),
                        PotType = c.String(nullable: false),
                        PhotoUrl = c.String(nullable: false),
                        Comments = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CollectionNumber)
                .ForeignKey("dbo.AcquisitionTypes", t => t.AcquisitionTypeName, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationName, cascadeDelete: true)
                .ForeignKey("dbo.Soils", t => t.SoilName, cascadeDelete: true)
                .ForeignKey("dbo.SpeciesLabels", t => t.SpeciesLabelName, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorName, cascadeDelete: true)
                .Index(t => t.LocationName)
                .Index(t => t.SpeciesLabelName)
                .Index(t => t.AcquisitionTypeName)
                .Index(t => t.VendorName)
                .Index(t => t.SoilName);
            
            CreateTable(
                "dbo.Soils",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Desription = c.String(),
                    })
                .PrimaryKey(t => t.Name)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.SpeciesLabels",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        FieldNumber = c.String(),
                        CustomGroupName = c.String(nullable: false, maxLength: 128),
                        FamilyName = c.String(nullable: false, maxLength: 128),
                        GenusName = c.String(nullable: false, maxLength: 128),
                        SpeciesName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.CustomGroups", t => t.CustomGroupName, cascadeDelete: true)
                .ForeignKey("dbo.Families", t => t.FamilyName, cascadeDelete: true)
                .ForeignKey("dbo.Genus", t => t.GenusName, cascadeDelete: true)
                .ForeignKey("dbo.Species", t => t.SpeciesName, cascadeDelete: true)
                .Index(t => t.CustomGroupName)
                .Index(t => t.FamilyName)
                .Index(t => t.GenusName)
                .Index(t => t.SpeciesName);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        WebSite = c.String(),
                    })
                .PrimaryKey(t => t.Name)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Plants", "VendorName", "dbo.Vendors");
            DropForeignKey("dbo.Plants", "SpeciesLabelName", "dbo.SpeciesLabels");
            DropForeignKey("dbo.SpeciesLabels", "SpeciesName", "dbo.Species");
            DropForeignKey("dbo.SpeciesLabels", "GenusName", "dbo.Genus");
            DropForeignKey("dbo.SpeciesLabels", "FamilyName", "dbo.Families");
            DropForeignKey("dbo.SpeciesLabels", "CustomGroupName", "dbo.CustomGroups");
            DropForeignKey("dbo.Plants", "SoilName", "dbo.Soils");
            DropForeignKey("dbo.Plants", "LocationName", "dbo.Locations");
            DropForeignKey("dbo.Plants", "AcquisitionTypeName", "dbo.AcquisitionTypes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Vendors", new[] { "Name" });
            DropIndex("dbo.Species", new[] { "Name" });
            DropIndex("dbo.SpeciesLabels", new[] { "SpeciesName" });
            DropIndex("dbo.SpeciesLabels", new[] { "GenusName" });
            DropIndex("dbo.SpeciesLabels", new[] { "FamilyName" });
            DropIndex("dbo.SpeciesLabels", new[] { "CustomGroupName" });
            DropIndex("dbo.Soils", new[] { "Name" });
            DropIndex("dbo.Plants", new[] { "SoilName" });
            DropIndex("dbo.Plants", new[] { "VendorName" });
            DropIndex("dbo.Plants", new[] { "AcquisitionTypeName" });
            DropIndex("dbo.Plants", new[] { "SpeciesLabelName" });
            DropIndex("dbo.Plants", new[] { "LocationName" });
            DropIndex("dbo.Locations", new[] { "Name" });
            DropIndex("dbo.Genus", new[] { "Name" });
            DropIndex("dbo.Families", new[] { "Name" });
            DropIndex("dbo.CustomGroups", new[] { "Name" });
            DropIndex("dbo.AcquisitionTypes", new[] { "Name" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Vendors");
            DropTable("dbo.Species");
            DropTable("dbo.SpeciesLabels");
            DropTable("dbo.Soils");
            DropTable("dbo.Plants");
            DropTable("dbo.Locations");
            DropTable("dbo.Genus");
            DropTable("dbo.Families");
            DropTable("dbo.CustomGroups");
            DropTable("dbo.AcquisitionTypes");
        }
    }
}
