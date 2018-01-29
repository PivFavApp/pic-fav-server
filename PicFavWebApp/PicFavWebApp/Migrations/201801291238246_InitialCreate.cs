namespace PicFavWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameImage",
                c => new
                    {
                        GameImageId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        Game_GameId = c.Int(),
                    })
                .PrimaryKey(t => t.GameImageId)
                .ForeignKey("dbo.Game", t => t.Game_GameId)
                .Index(t => t.Game_GameId);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        PublicId = c.String(),
                        Date = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        PublicId = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Age = c.Long(nullable: false),
                        Rating = c.Int(nullable: false),
                        AvatarUrl = c.String(),
                        Role = c.Int(nullable: false),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameImage", "Game_GameId", "dbo.Game");
            DropIndex("dbo.GameImage", new[] { "Game_GameId" });
            DropTable("dbo.User");
            DropTable("dbo.Game");
            DropTable("dbo.GameImage");
        }
    }
}
