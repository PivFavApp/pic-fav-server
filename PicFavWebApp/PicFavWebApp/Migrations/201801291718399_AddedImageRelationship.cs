namespace PicFavWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameImage", "Game_GameId", "dbo.Game");
            DropIndex("dbo.GameImage", new[] { "Game_GameId" });
            RenameColumn(table: "dbo.GameImage", name: "Game_GameId", newName: "GameId");
            AlterColumn("dbo.GameImage", "GameId", c => c.Int(nullable: false));
            CreateIndex("dbo.GameImage", "GameId");
            AddForeignKey("dbo.GameImage", "GameId", "dbo.Game", "GameId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameImage", "GameId", "dbo.Game");
            DropIndex("dbo.GameImage", new[] { "GameId" });
            AlterColumn("dbo.GameImage", "GameId", c => c.Int());
            RenameColumn(table: "dbo.GameImage", name: "GameId", newName: "Game_GameId");
            CreateIndex("dbo.GameImage", "Game_GameId");
            AddForeignKey("dbo.GameImage", "Game_GameId", "dbo.Game", "GameId");
        }
    }
}
