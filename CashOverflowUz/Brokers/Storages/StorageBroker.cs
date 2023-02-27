using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CashOverflowUz.Brokers.Storages
{
    public partial class StorageBroker:EFxceptionsContext
    {
        public readonly IConfiguration Configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                this.Configuration.GetConnectionString(name: "DefaultConnection");
           
            optionsBuilder.UseSqlServer(connectionString);  
        }
    }
}
