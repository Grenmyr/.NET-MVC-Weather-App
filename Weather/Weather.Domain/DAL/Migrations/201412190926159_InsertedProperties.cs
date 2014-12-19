namespace Weather.Domain.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertedProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("appSchema.Forecast", "Cloudiness", c => c.String(maxLength: 6));
            AddColumn("appSchema.Location", "Timestamp", c => c.DateTime(nullable: false));
            AddColumn("appSchema.Location", "geonameId", c => c.Int(nullable: false));
            AlterColumn("appSchema.Forecast", "Temperature", c => c.String(maxLength: 4));
            AlterColumn("appSchema.Forecast", "NederBird", c => c.String(maxLength: 6));
            AlterColumn("appSchema.Forecast", "SymbolId", c => c.Int(nullable: false));
            DropColumn("appSchema.Forecast", "CloudFactor");
        }
        
        public override void Down()
        {
            AddColumn("appSchema.Forecast", "CloudFactor", c => c.Int(nullable: false));
            AlterColumn("appSchema.Forecast", "SymbolId", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("appSchema.Forecast", "NederBird", c => c.Int(nullable: false));
            AlterColumn("appSchema.Forecast", "Temperature", c => c.Int(nullable: false));
            DropColumn("appSchema.Location", "geonameId");
            DropColumn("appSchema.Location", "Timestamp");
            DropColumn("appSchema.Forecast", "Cloudiness");
        }
    }
}
