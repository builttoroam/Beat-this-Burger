namespace BeatThisBurger.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotNullRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("BeatThisBurger.Ratings", "Rate", c => c.Double(nullable: false));
            DropColumn("BeatThisBurger.Ratings", "Value");
        }
        
        public override void Down()
        {
            AddColumn("BeatThisBurger.Ratings", "Value", c => c.Double());
            DropColumn("BeatThisBurger.Ratings", "Rate");
        }
    }
}
