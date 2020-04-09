namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eliminateRolefifth : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "role5");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "role5", c => c.String());
        }
    }
}
