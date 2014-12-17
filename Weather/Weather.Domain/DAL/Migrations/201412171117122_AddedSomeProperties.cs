namespace Weather.Domain.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSomeProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("appSchema.Forecast", "Location_Id", "appSchema.Location");
            DropIndex("appSchema.Forecast", new[] { "Location_Id" });
            RenameColumn(table: "appSchema.Forecast", name: "Location_Id", newName: "LocationId");
            AlterColumn("appSchema.Forecast", "LocationId", c => c.Int(nullable: false));
            CreateIndex("appSchema.Forecast", "LocationId");
            AddForeignKey("appSchema.Forecast", "LocationId", "appSchema.Location", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("appSchema.Forecast", "LocationId", "appSchema.Location");
            DropIndex("appSchema.Forecast", new[] { "LocationId" });
            AlterColumn("appSchema.Forecast", "LocationId", c => c.Int());
            RenameColumn(table: "appSchema.Forecast", name: "LocationId", newName: "Location_Id");
            CreateIndex("appSchema.Forecast", "Location_Id");
            AddForeignKey("appSchema.Forecast", "Location_Id", "appSchema.Location", "Id");
        }
    }
}
