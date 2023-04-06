//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using CashOverflowUz.Models.Reviews;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflowUz.Services.Foundetions.Reviews
{
    public interface IReviewService
    {
        ValueTask<Review> AddReviewAsync(Review review);
        IQueryable<Review> RetrieveAllReviews();
    }
}
