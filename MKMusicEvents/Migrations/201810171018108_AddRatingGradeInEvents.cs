namespace MKMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRatingGradeInEvents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventRatingGrade", c => c.Double(nullable: false));
            DropColumn("dbo.Events", "RatingGrade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "RatingGrade", c => c.Double(nullable: false));
            DropColumn("dbo.Events", "EventRatingGrade");
        }
    }
}
