namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editbookStatue : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShowSeats", "BookingStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShowSeats", "BookingStatus", c => c.Boolean(nullable: false));
        }
    }
}
