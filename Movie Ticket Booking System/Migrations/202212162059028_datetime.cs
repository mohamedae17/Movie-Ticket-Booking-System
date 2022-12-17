namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShowSeats", "createdOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShowSeats", "createdOn", c => c.DateTime(nullable: false));
        }
    }
}
