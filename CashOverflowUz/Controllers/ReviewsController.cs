
//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using CashOverflowUz.Models.Reviews;
using CashOverflowUz.Models.Reviews.Exceptions;
using CashOverflowUz.Services.Foundetions.Reviews;

namespace CashOverflowUz.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	public class ReviewsController : RESTFulController
	{
		private readonly IReviewService reviewService;

		public ReviewsController(IReviewService reviewService) =>
			this.reviewService = reviewService;

		[HttpPost]
		public async ValueTask<ActionResult<Review>> PostReviewAsync(Review review)
		{
			try
			{
				return await this.reviewService.AddReviewAsync(review);
			}
			catch (ReviewValidationException reviewValidationException)
			{
				return BadRequest(reviewValidationException.InnerException);
			}
			catch (ReviewDependencyValidationException reviewDependencyValidationException)
				when (reviewDependencyValidationException.InnerException is AlreadyExistsReviewException)
			{
				return Conflict(reviewDependencyValidationException.InnerException);
			}
			catch (ReviewDependencyValidationException reviewDependencyValidationException)
			{
				return BadRequest(reviewDependencyValidationException.InnerException);
			}
			catch (ReviewDependencyException reviewDependencyException)
			{
				return InternalServerError(reviewDependencyException.InnerException);
			}
			catch (ReviewServiceException reviewServiceException)
			{
				return InternalServerError(reviewServiceException.InnerException);
			}
		}

		[HttpGet]
		public ActionResult<IQueryable<Review>> GetAllReviews()
		{
			try
			{
				IQueryable<Review> allReviews = this.reviewService.RetrieveAllReviews();

				return Ok(allReviews);
			}
			catch (ReviewDependencyException reviewDependencyException)
			{
				return InternalServerError(reviewDependencyException.InnerException);
			}
			catch (ReviewServiceException reviewServiceException)
			{
				return InternalServerError(reviewServiceException.InnerException);
			}
		}
	}
}
