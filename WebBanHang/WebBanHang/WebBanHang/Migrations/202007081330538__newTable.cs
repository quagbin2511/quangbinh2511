namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _newTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        IDCategory = c.Int(nullable: false, identity: true),
                        NameCategory = c.String(),
                        Category_IDCategory = c.Int(),
                    })
                .PrimaryKey(t => t.IDCategory)
                .ForeignKey("dbo.Categories", t => t.Category_IDCategory)
                .Index(t => t.Category_IDCategory);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        IDProduct = c.Int(nullable: false, identity: true),
                        NameProduct = c.String(),
                        UnitPrice = c.Int(),
                        Images = c.String(),
                        ProductDate = c.DateTime(),
                        Available = c.String(),
                        Descriptions = c.String(),
                        Quantity = c.Int(),
                        IDCategory = c.Int(),
                    })
                .PrimaryKey(t => t.IDProduct)
                .ForeignKey("dbo.Categories", t => t.IDCategory)
                .Index(t => t.IDCategory);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UnitPriceSale = c.Decimal(precision: 18, scale: 2),
                        QuantitySale = c.Int(),
                        IDOrder = c.Int(),
                        IDProduct = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.IDOrder)
                .ForeignKey("dbo.Products", t => t.IDProduct)
                .Index(t => t.IDOrder)
                .Index(t => t.IDProduct);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        IDOrder = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(),
                        Descriptions = c.String(),
                        CodeCus = c.Int(),
                    })
                .PrimaryKey(t => t.IDOrder)
                .ForeignKey("dbo.Customers", t => t.CodeCus)
                .Index(t => t.CodeCus);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        IDCus = c.Int(nullable: false, identity: true),
                        CodeCus = c.Int(nullable: false),
                        Email_Cus = c.String(),
                        Address_Cus = c.String(),
                        Phone_Cus = c.String(),
                    })
                .PrimaryKey(t => t.IDCus);
            
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
                        Address = c.String(),
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
            DropForeignKey("dbo.OrderDetails", "IDProduct", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "IDOrder", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CodeCus", "dbo.Customers");
            DropForeignKey("dbo.Products", "IDCategory", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Category_IDCategory", "dbo.Categories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Orders", new[] { "CodeCus" });
            DropIndex("dbo.OrderDetails", new[] { "IDProduct" });
            DropIndex("dbo.OrderDetails", new[] { "IDOrder" });
            DropIndex("dbo.Products", new[] { "IDCategory" });
            DropIndex("dbo.Categories", new[] { "Category_IDCategory" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
