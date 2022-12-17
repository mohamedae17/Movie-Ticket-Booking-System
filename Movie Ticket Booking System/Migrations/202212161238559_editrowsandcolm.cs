namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editrowsandcolm : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShowSeats", "seatNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShowSeats", "seatNumber", c => c.Int(nullable: false));
        }
    }
}
