using Microsoft.EntityFrameworkCore;
using UnoService.Models;

namespace UnoService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {

            if(isProd)
            {
                Console.WriteLine("--> Attempting to apply migration...");
                try{
                    context.Database.Migrate();
                }catch(Exception ex){
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
                
            }else{

            }

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