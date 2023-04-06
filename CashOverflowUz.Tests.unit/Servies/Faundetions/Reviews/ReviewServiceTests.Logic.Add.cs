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
		public async Task ShouldAddReviewAsync()
		{
			// given
			int randomStars = GetRandomStarsInRange();
			Review randomReview = CreateRandomReview(randomStars);
			Review inputReview = randomReview;
			Review persistedReview = inputReview;
			Review expectedReview = persistedReview.DeepClone();

			this.storageBrokerMock.Setup(broker =>
				broker.InsertReviewAsync(inputReview)).ReturnsAsync(persistedReview);

			// when
			Review actualReview = await this.reviewService
				.AddReviewAsync(inputReview);

			// then
			actualReview.Should().BeEquivalentTo(expectedReview);

			this.storageBrokerMock.Verify(broker =>
				broker.InsertReviewAsync(inputReview), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}
	}
}
