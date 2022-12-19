namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editMovieModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieDetails", "RleaseDate", c => c.String());
            DropColumn("dbo.MovieDetails", "DateAndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MovieDetails", "DateAndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.MovieDetails", "RleaseDate");
        }
    }
}
