namespace MKMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedQuantityToEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Quantity");
        }
    }
}
