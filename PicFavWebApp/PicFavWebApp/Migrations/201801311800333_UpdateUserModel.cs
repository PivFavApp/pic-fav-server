namespace PicFavWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "GeneralRating", c => c.Int(nullable: false));
            AddColumn("dbo.User", "AverageRating", c => c.Int(nullable: false));
            DropColumn("dbo.User", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Rating", c => c.Int(nullable: false));
            DropColumn("dbo.User", "AverageRating");
            DropColumn("dbo.User", "GeneralRating");
        }
    }
}
