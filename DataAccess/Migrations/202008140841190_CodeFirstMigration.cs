namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CodeFirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Intro = c.String(maxLength: 200),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Employees",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Designation = c.String(nullable: false, maxLength: 50, unicode: false),
                    Salary = c.Int(nullable: false),
                    MobileNumber = c.String(nullable: false, maxLength: 50, unicode: false),
                    Name = c.String(maxLength: 20, unicode: false),
                })
                .PrimaryKey(t => new { t.Id, t.Designation, t.Salary, t.MobileNumber });

            CreateTable(
                "dbo.MovieActors",
                c => new
                {
                    MovieId = c.Int(nullable: false),
                    ActorId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.MovieId, t.ActorId });

            CreateTable(
                "dbo.Movies",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(maxLength: 50, unicode: false),
                    ReleaseDate = c.DateTime(nullable: false, storeType: "date"),
                    Genre = c.String(maxLength: 50, unicode: false),
                    Price = c.Decimal(precision: 18, scale: 0),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TodoItem",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 60, unicode: false),
                    IsComplete = c.Boolean(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.TodoItem");
            DropTable("dbo.Movies");
            DropTable("dbo.MovieActors");
            DropTable("dbo.Employees");
            DropTable("dbo.Actors");
        }
    }
}
