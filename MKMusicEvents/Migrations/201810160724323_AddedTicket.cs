namespace MKMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTicket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardName = c.String(nullable: false),
                        CardNumber = c.Int(nullable: false),
                        ExpiryDateMonth = c.Int(nullable: false),
                        ExpiryDateYear = c.Int(nullable: false),
                        SecurityCode = c.Int(nullable: false),
                        CardholderName = c.String(nullable: false),
                        CardholderLastName = c.String(nullable: false),
                        SaveCard = c.Boolean(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tickets");
        }
    }
}
