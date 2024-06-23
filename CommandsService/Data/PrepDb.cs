using CommandsService.Models;
using CommandsService.SyncDataService.Grpc;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IUnoDataClient>();

                var unos = grpcClient.ReturnAllUno();

                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), unos);
            }
        }

        private static void SeedData(ICommandRepo repo, IEnumerable<Uno> unos)
        {
            Console.WriteLine("--> Seeding new Uno objects");

            foreach(var uno in unos)
            {
                if(!repo.ExternalUnoExist(uno.ExternalId))
                {
                    repo.CreateUno(uno);

                }

                repo.SaveChages();
            }
        }
    }
}