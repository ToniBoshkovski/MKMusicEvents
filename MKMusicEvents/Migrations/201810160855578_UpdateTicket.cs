namespace MKMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTicket : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tickets", "CardholderLastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "CardholderLastName", c => c.String(nullable: false));
        }
    }
}
