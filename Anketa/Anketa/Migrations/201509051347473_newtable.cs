namespace Anketa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfileInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        userName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "UserProfileInfo_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "UserProfileInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "UserProfileInfo_Id", "dbo.UserProfileInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserProfileInfo_Id", "dbo.UserProfileInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "UserProfileInfo_Id" });
            DropColumn("dbo.AspNetUsers", "UserProfileInfo_Id");
            DropTable("dbo.UserProfileInfoes");
        }
    }
}
