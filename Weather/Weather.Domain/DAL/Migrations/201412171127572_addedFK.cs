namespace Weather.Domain.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFK : DbMigration
    {
        public override void Up()
        {
            AlterColumn("appSchema.Forecast", "SymbolId", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("appSchema.Location", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("appSchema.Location", "Name", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("appSchema.Forecast", "SymbolId", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
