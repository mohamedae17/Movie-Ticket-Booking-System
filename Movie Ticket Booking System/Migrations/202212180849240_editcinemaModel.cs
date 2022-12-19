namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editcinemaModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Cinemas", name: "CinemaId", newName: "CityId");
            RenameIndex(table: "dbo.Cinemas", name: "IX_CinemaId", newName: "IX_CityId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Cinemas", name: "IX_CityId", newName: "IX_CinemaId");
            RenameColumn(table: "dbo.Cinemas", name: "CityId", newName: "CinemaId");
        }
    }
}
