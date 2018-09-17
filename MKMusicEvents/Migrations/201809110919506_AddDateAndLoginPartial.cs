namespace MKMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateAndLoginPartial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Date");
        }
    }
}
