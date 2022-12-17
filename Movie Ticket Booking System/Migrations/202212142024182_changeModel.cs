namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShowSeats", "price");
            DropColumn("dbo.ShowSeats", "numberOfSeats");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShowSeats", "numberOfSeats", c => c.Int(nullable: false));
            AddColumn("dbo.ShowSeats", "price", c => c.Double(nullable: false));
        }
    }
}
