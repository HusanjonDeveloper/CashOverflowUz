using System.Threading.Tasks;
using CashOverflowUz.Models.job;
using Microsoft.EntityFrameworkCore;

namespace CashOverflowUz.Brokers.Storages
{
	public partial class StorageBroker
	{
		public DbSet<Job> Jobs { get; set; }
		public async ValueTask<Job> InsertJobAsync(Job job) =>
			 await InsertAsync(job);
	}
}
