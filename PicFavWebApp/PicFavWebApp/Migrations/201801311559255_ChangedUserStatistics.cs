namespace PicFavWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUserStatistics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserStatistic",
                c => new
                    {
                        UserStatisticId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GamePublicId = c.String(),
                        Date = c.Long(nullable: false),
                        Result = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserStatisticId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropTable("dbo.Statistics");
        }
        
        public override void Down()
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
            
            DropForeignKey("dbo.UserStatistic", "UserId", "dbo.User");
            DropIndex("dbo.UserStatistic", new[] { "UserId" });
            DropTable("dbo.UserStatistic");
        }
    }
}
