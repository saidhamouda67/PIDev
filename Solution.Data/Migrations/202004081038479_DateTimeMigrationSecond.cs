namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeMigrationSecond : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "MemberSince", c => c.DateTime(defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "MemberSince", c => c.DateTime(nullable: false));
        }
    }
}
