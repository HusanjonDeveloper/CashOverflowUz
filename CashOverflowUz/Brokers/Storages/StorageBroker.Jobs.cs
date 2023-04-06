using System.Linq;
using System;
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

		public IQueryable<Job> SelectAllJobs() =>
			SelectAll<Job>();

		public async ValueTask<Job> SelectJobByIdAsync(Guid jobId) =>
			await SelectAsync<Job>(jobId);

		public async ValueTask<Job> UpdateJobAsync(Job job) =>
			await UpdateAsync<Job>(job);

		public async ValueTask<Job> DeleteJobAsync(Job job) =>
			await DeleteAsync<Job>(job);
	}
}
