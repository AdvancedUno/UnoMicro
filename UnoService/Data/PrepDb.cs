using UnoService.Models;

namespace UnoService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Unos.Any()){
                Console.WriteLine("--> Seeding Data");

                context.Unos.AddRange(
                    new Uno(){
                        Name = "Dot Net",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Uno(){
                        Name = "SQL",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}