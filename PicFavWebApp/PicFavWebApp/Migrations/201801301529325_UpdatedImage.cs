namespace PicFavWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedImage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.GameImage", "ImageBlob");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameImage", "ImageBlob", c => c.Binary());
        }
    }
}
