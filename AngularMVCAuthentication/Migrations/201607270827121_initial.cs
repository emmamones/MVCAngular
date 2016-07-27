namespace AngularMVCAuthentication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.People", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "LastName", c => c.String());
        }
    }
}
