namespace AngularMVCAuthentication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSeccion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Eventoes", "SeccionEventoId", "dbo.SeccionEventoes");
            DropIndex("dbo.Eventoes", new[] { "SeccionEventoId" });
            AddColumn("dbo.Eventoes", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Eventoes", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            DropColumn("dbo.Eventoes", "SeccionEventoId");
            DropTable("dbo.SeccionEventoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SeccionEventoes",
                c => new
                    {
                        SeccionEventoId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SeccionEventoId);
            
            AddColumn("dbo.Eventoes", "SeccionEventoId", c => c.Int(nullable: false));
            DropColumn("dbo.Eventoes", "RowVersion");
            DropColumn("dbo.Eventoes", "IsDeleted");
            CreateIndex("dbo.Eventoes", "SeccionEventoId");
            AddForeignKey("dbo.Eventoes", "SeccionEventoId", "dbo.SeccionEventoes", "SeccionEventoId", cascadeDelete: true);
        }
    }
}
