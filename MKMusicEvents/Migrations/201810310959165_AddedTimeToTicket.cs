namespace MKMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTimeToTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Time", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "Time");
        }
    }
}
