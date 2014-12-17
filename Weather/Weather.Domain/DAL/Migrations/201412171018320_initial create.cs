namespace Weather.Domain.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "appSchema.Forecast",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        temperature = c.Int(nullable: false),
                        cloudFactor = c.Int(nullable: false),
                        NederBird = c.Int(nullable: false),
                        symbolId = c.String(nullable: false, maxLength: 50, unicode: false),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("appSchema.Location", t => t.Location_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "appSchema.Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        lat = c.String(nullable: false, maxLength: 25),
                        lng = c.String(nullable: false, maxLength: 25),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("appSchema.Forecast", "Location_Id", "appSchema.Location");
            DropIndex("appSchema.Forecast", new[] { "Location_Id" });
            DropTable("appSchema.Location");
            DropTable("appSchema.Forecast");
        }
    }
}
