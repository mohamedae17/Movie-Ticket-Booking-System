namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class frontendofficetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FrontEndOfficers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        seatRow = c.Int(nullable: false),
                        seatColum = c.Int(nullable: false),
                        isReserved = c.Boolean(nullable: false),
                        BookingNumber = c.String(),
                        createdOn = c.DateTime(),
                        ShowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shows", t => t.ShowId, cascadeDelete: true)
                .Index(t => t.ShowId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FrontEndOfficers", "ShowId", "dbo.Shows");
            DropIndex("dbo.FrontEndOfficers", new[] { "ShowId" });
            DropTable("dbo.FrontEndOfficers");
        }
    }
}
