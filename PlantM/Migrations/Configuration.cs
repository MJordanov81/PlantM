namespace PlantM.Migrations
{
    using System.Data.Entity.Migrations;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<PlantM.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "PlantM.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {

        }
    }
}
