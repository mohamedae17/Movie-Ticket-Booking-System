namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieName = c.String(),
                        MovieDescription = c.String(),
                        DateAndTime = c.DateTime(nullable: false),
                        MoviePicture = c.String(),
                        CinemaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cinemas", t => t.CinemaId, cascadeDelete: true)
                .Index(t => t.CinemaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieDetails", "CinemaId", "dbo.Cinemas");
            DropIndex("dbo.MovieDetails", new[] { "CinemaId" });
            DropTable("dbo.MovieDetails");
        }
    }
}
