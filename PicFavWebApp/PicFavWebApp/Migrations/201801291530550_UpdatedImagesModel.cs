namespace PicFavWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedImagesModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameImage", "ImageBlob", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameImage", "ImageBlob");
        }
    }
}
