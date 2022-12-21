namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coupon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FrontEndOfficers", "Coupon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FrontEndOfficers", "Coupon");
        }
    }
}
