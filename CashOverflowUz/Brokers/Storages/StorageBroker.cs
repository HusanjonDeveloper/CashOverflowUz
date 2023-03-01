using System.Threading.Tasks;
using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CashOverflowUz.Brokers.Storages
{
    public partial class StorageBroker:EFxceptionsContext, IStorageBroker
    {
        public readonly IConfiguration Configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.Database.Migrate();
        }

      public async ValueTask<T> InsertAsync<T>(T@object)
        {
            var broker = new StorageBroker(this.Configuration);
            broker.Entry(@object).State = EntityState.Added;
            return @object;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                this.Configuration.GetConnectionString(name: "DefaultConnection");
           
            optionsBuilder.UseSqlServer(connectionString);  
        }
    }
}
