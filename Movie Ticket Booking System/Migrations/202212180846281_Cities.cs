namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cinemas", "CinemaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cinemas", "CinemaId");
            AddForeignKey("dbo.Cinemas", "CinemaId", "dbo.Cities", "Id", cascadeDelete: true);
            DropColumn("dbo.Cinemas", "CinemaCity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cinemas", "CinemaCity", c => c.String(nullable: false));
            DropForeignKey("dbo.Cinemas", "CinemaId", "dbo.Cities");
            DropIndex("dbo.Cinemas", new[] { "CinemaId" });
            DropColumn("dbo.Cinemas", "CinemaId");
            DropTable("dbo.Cities");
        }
    }
}
