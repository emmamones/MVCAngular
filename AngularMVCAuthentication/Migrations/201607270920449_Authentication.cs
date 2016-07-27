namespace AngularMVCAuthentication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Authentication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PeopleId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        Retired = c.Boolean(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.PeopleId);
            
            DropTable("dbo.People");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        Retired = c.Boolean(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.PersonId);
            
            DropTable("dbo.People");
        }
    }
}
