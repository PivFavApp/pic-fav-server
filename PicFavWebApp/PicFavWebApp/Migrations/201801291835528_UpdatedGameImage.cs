namespace PicFavWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedGameImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameImage", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameImage", "ImageName");
        }
    }
}
