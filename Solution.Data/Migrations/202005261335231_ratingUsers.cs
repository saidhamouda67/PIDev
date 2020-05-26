namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "ratingUsers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "ratingUsers");
        }
    }
}
