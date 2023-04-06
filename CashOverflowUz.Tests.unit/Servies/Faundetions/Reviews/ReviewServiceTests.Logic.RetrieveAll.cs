// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashOverflowUz.Models.Reviews;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Reviews
{
	public partial class ReviewServiceTests
	{
		[Fact]
		public void ShouldRetrieveAllReviews()
		{
			// given
			IQueryable<Review> randomReviews = (IQueryable<Review>)CreateRandomReviews();
			IQueryable<Review> storageReviews = randomReviews;
			IQueryable<Review> expectedReveiws = storageReviews.DeepClone();

			this.storageBrokerMock.Setup(broker =>
				broker.SelectAllReviews()).Returns(storageReviews);
			// when
			IQueryable<Review> actualReviews =
				this.reviewService.RetrieveAllReviews();

			// then
			actualReviews.Should().BeEquivalentTo(expectedReveiws);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectAllReviews(), Times.Once());

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}
	}
}
