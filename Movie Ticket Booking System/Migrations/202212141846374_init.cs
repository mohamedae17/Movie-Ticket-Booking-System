namespace Movie_Ticket_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cinemas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CinemaName = c.String(nullable: false),
                        CinemaCity = c.String(nullable: false),
                        totalHalls = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        totalSeats = c.Int(nullable: false),
                        CinemaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cinemas", t => t.CinemaId, cascadeDelete: true)
                .Index(t => t.CinemaId);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        createOn = c.DateTime(nullable: false),
                        startTime = c.DateTime(nullable: false),
                        endDate = c.DateTime(nullable: false),
                        HallId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Halls", t => t.HallId, cascadeDelete: true)
                .ForeignKey("dbo.MovieDetails", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.HallId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.MovieDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieName = c.String(nullable: false),
                        MovieDescription = c.String(),
                        durationMinutes = c.Int(nullable: false),
                        Language = c.String(),
                        DateAndTime = c.DateTime(nullable: false),
                        Country = c.String(),
                        genre = c.String(),
                        MoviePicture = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShowSeats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        seatRow = c.Int(nullable: false),
                        seatColum = c.Int(nullable: false),
                        seatNumber = c.Int(nullable: false),
                        isReserved = c.Boolean(nullable: false),
                        price = c.Double(nullable: false),
                        numberOfSeats = c.Int(nullable: false),
                        BookingNumber = c.String(),
                        createdOn = c.DateTime(nullable: false),
                        BookingStatus = c.Boolean(nullable: false),
                        ShowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shows", t => t.ShowId, cascadeDelete: true)
                .Index(t => t.ShowId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ShowSeats", "ShowId", "dbo.Shows");
            DropForeignKey("dbo.Shows", "MovieId", "dbo.MovieDetails");
            DropForeignKey("dbo.Shows", "HallId", "dbo.Halls");
            DropForeignKey("dbo.Halls", "CinemaId", "dbo.Cinemas");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ShowSeats", new[] { "ShowId" });
            DropIndex("dbo.Shows", new[] { "MovieId" });
            DropIndex("dbo.Shows", new[] { "HallId" });
            DropIndex("dbo.Halls", new[] { "CinemaId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ShowSeats");
            DropTable("dbo.MovieDetails");
            DropTable("dbo.Shows");
            DropTable("dbo.Halls");
            DropTable("dbo.Cinemas");
        }
    }
}
