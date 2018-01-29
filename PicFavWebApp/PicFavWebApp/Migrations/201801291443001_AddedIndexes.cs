namespace PicFavWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIndexes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Game", "PublicId", c => c.String(maxLength: 450));
            AlterColumn("dbo.Game", "Name", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.User", "PublicId", c => c.String(maxLength: 450));
            AlterColumn("dbo.User", "UserName", c => c.String(nullable: false, maxLength: 450));
            CreateIndex("dbo.Game", "PublicId", unique: true, name: "IX_GamePublicId");
            CreateIndex("dbo.Game", "Date", unique: true, name: "IX_GameDate");
            CreateIndex("dbo.Game", "Name", unique: true, name: "IX_GameName");
            CreateIndex("dbo.User", "PublicId", unique: true, name: "IX_UserPublicId");
            CreateIndex("dbo.User", "UserName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "UserName" });
            DropIndex("dbo.User", "IX_UserPublicId");
            DropIndex("dbo.Game", "IX_GameName");
            DropIndex("dbo.Game", "IX_GameDate");
            DropIndex("dbo.Game", "IX_GamePublicId");
            AlterColumn("dbo.User", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.User", "PublicId", c => c.String());
            AlterColumn("dbo.Game", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Game", "PublicId", c => c.String());
        }
    }
}
