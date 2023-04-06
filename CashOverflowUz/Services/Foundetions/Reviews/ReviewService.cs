//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System.Linq;
using System.Threading.Tasks;
using CashOverflow.Brokers.Storages;
using CashOverflowUz.Brokers.DateTimes;
using CashOverflowUz.Brokers.Loggings;
using CashOverflowUz.Models.Reviews;
using CashOverflowUz.Services.Foundetions.Reviews;

namespace CashOverflowUz.Services.Reviews
{
    public partial class ReviewService : IReviewService
	{
		private readonly IStorageBroker storageBroker;
		private readonly ILoggingBroker loggingBroker;
		private readonly IDateTimeBroker dateTimeBroker;

		public ReviewService(
			IStorageBroker storageBroker,
			ILoggingBroker loggingBroker,
			IDateTimeBroker dateTimeBroker)
		{
			this.storageBroker = storageBroker;
			this.loggingBroker = loggingBroker;
			this.dateTimeBroker = dateTimeBroker;
		}

		public ValueTask<Review> AddReviewAsync(Review review) =>
		TryCatch(async () =>
		{
			ValidateReviewOnAdd(review);

			return await this.storageBroker.InsertReviewAsync(review);
		});

		public IQueryable<Review> RetrieveAllReviews() =>
			TryCatch(() => this.storageBroker.SelectAllReviews());
	}
}
