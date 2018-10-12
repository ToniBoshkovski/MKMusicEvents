namespace MKMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateEvent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Date", c => c.String(nullable: false));
        }
    }
}
