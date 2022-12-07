namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solve2AndLanguages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieDetails", "Language", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovieDetails", "Language");
        }
    }
}
