namespace Weather.Domain.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedAgain : DbMigration
    {
        public override void Up()
        {
            DropIndex("appSchema.Forecast", new[] { "Location_id" });
            CreateIndex("appSchema.Forecast", "Location_Id");
        }
        
        public override void Down()
        {
            DropIndex("appSchema.Forecast", new[] { "Location_Id" });
            CreateIndex("appSchema.Forecast", "Location_id");
        }
    }
}
