namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "MemberSince", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "MemberSince", c => c.DateTime(nullable: false));
        }
    }
}
