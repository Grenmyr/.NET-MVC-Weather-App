namespace Weather.Domain.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAdminName1andCountryName : DbMigration
    {
        public override void Up()
        {
            AddColumn("appSchema.Location", "AdminName1", c => c.String(nullable: false, maxLength: 100));
            AddColumn("appSchema.Location", "CountryName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("appSchema.Location", "geonameId");
        }
        
        public override void Down()
        {
            AddColumn("appSchema.Location", "geonameId", c => c.Int(nullable: false));
            DropColumn("appSchema.Location", "CountryName");
            DropColumn("appSchema.Location", "AdminName1");
        }
    }
}
