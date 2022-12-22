namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FrontEndOfficers", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.FrontEndOfficers", "UserId");
            AddForeignKey("dbo.FrontEndOfficers", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FrontEndOfficers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.FrontEndOfficers", new[] { "UserId" });
            DropColumn("dbo.FrontEndOfficers", "UserId");
        }
    }
}
