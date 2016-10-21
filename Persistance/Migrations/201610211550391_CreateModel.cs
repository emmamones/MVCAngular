namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        BirthDate = c.DateTime(),
                        IsSubscribedToNewsLetter = c.Boolean(nullable: false),
                        MembershipTypeId = c.Int(nullable: false),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Updated = c.DateTime(),
                        UpdatedBy = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MembershipTypes", t => t.MembershipTypeId, cascadeDelete: true)
                .Index(t => t.MembershipTypeId);
            
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        SignUpFee = c.Short(nullable: false),
                        DurationInMonths = c.Byte(nullable: false),
                        DiscountRate = c.Byte(nullable: false),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Updated = c.DateTime(),
                        UpdatedBy = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Eventoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DateEvent = c.DateTime(),
                        Location = c.String(),
                        URL = c.String(),
                        Recommendation = c.String(),
                        ApplicationUserName = c.String(),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Updated = c.DateTime(),
                        UpdatedBy = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Updated = c.DateTime(),
                        UpdatedBy = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        GenreId = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        ArrivalDate = c.DateTime(nullable: false),
                        DirectorName = c.String(),
                        NumberInStock = c.Byte(nullable: false),
                        NumberAvailable = c.Byte(nullable: false),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Updated = c.DateTime(),
                        UpdatedBy = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RentDate = c.DateTime(),
                        ReturnDate = c.DateTime(),
                        TimeStamp = c.String(),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Updated = c.DateTime(),
                        UpdatedBy = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Customer_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Rentals", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Movies", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Customers", "MembershipTypeId", "dbo.MembershipTypes");
            DropIndex("dbo.Rentals", new[] { "Movie_Id" });
            DropIndex("dbo.Rentals", new[] { "Customer_Id" });
            DropIndex("dbo.Movies", new[] { "GenreId" });
            DropIndex("dbo.Customers", new[] { "MembershipTypeId" });
            DropTable("dbo.Rentals");
            DropTable("dbo.Movies");
            DropTable("dbo.Genres");
            DropTable("dbo.Eventoes");
            DropTable("dbo.MembershipTypes");
            DropTable("dbo.Customers");
        }
    }
}
