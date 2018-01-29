namespace PicFavWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        StatisticsId = c.Int(nullable: false, identity: true),
                        UserPublicId = c.String(),
                        GamePublicId = c.String(),
                        Date = c.Long(nullable: false),
                        Result = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatisticsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Statistics");
        }
    }
}
