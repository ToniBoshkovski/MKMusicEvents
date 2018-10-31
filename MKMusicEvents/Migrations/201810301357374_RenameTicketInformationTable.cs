namespace MKMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTicketInformationTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Tickets", newName: "TicketInformations");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TicketInformations", newName: "Tickets");
        }
    }
}
