namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CashOrCredit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FrontEndOfficers", "PayWay", c => c.String());
            DropColumn("dbo.FrontEndOfficers", "seatColum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FrontEndOfficers", "seatColum", c => c.Int(nullable: false));
            DropColumn("dbo.FrontEndOfficers", "PayWay");
        }
    }
}
