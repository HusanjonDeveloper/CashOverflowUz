//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System.Linq;
using System.Threading.Tasks;
using CashOverflowUz.Models.Reviews;

namespace CashOverflowUz.Brokers.Storages
{
	public partial interface IStorageBroker
	{
		ValueTask<Review> InsertReviewAsync(Review review);
		IQueryable<Review> SelectAllReviews();
	}
}
