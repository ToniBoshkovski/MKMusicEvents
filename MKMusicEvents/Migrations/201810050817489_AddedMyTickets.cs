namespace MKMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMyTickets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EventId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MyTickets");
        }
    }
}