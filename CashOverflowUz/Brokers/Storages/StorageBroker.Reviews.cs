//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using CashOverflowUz.Models.Reviews;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflowUz.Brokers.Storages
{
	public partial class StorageBroker
	{
		public DbSet<Review> Reviews { get; set; }

		public async ValueTask<Review> InsertReviewAsync(Review review) =>
			await InsertAsync(review);

		public IQueryable<Review> SelectAllReviews() => SelectAll<Review>();
	}
}
