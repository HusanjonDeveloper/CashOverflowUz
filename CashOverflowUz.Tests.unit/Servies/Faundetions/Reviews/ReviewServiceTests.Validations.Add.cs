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
using CashOverflowUz.Models.Reviews.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Reviews
{
	public partial class ReviewServiceTests
	{
		[Fact]
		public async Task ShouldThrowValidationExceptionOnAddIfInputIsNullAndLogItAsync()
		{
			// given
			Review nullReview = null;
			var nullReviewException = new NullReviewException();

			var expectedReviewValidationException =
				new ReviewValidationException(nullReviewException);

			// when 
			ValueTask<Review> addReviewTask =
				this.reviewService.AddReviewAsync(nullReview);

			ReviewValidationException actualReviewValidationException =
				await Assert.ThrowsAsync<ReviewValidationException>(addReviewTask.AsTask);

			// then
			actualReviewValidationException.Should()
				.BeEquivalentTo(expectedReviewValidationException);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(expectedReviewValidationException)))
					, Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.InsertReviewAsync(It.IsAny<Review>()), Times.Never);

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task ShouldThrowValidationExceptionOnAddIfReviewIsInvalidAndLogItAsync(
			string invalidText)
		{
			// given
			Review invalidReview = new Review()
			{
				CompanyName = invalidText,
				Thoughts = invalidText
			};

			var invalidReviewException = new InvalidReviewException();

			invalidReviewException.AddData(
				key: nameof(Review.Id),
				values: "Id is required");

			invalidReviewException.AddData(
				key: nameof(Review.CompanyName),
				values: "Text is required");

			invalidReviewException.AddData(
				key: nameof(Review.Stars),
				values: "Stars are required");

			invalidReviewException.AddData(
				key: nameof(Review.Thoughts),
				values: "Text is required");

			var expectedReviewValidationException =
				new ReviewValidationException(invalidReviewException);

			// when
			ValueTask<Review> addReviewTask = this.reviewService.AddReviewAsync(invalidReview);

			ReviewValidationException actualReviewValidationException =
				await Assert.ThrowsAsync<ReviewValidationException>(addReviewTask.AsTask);

			// then
			actualReviewValidationException.Should()
				.BeEquivalentTo(expectedReviewValidationException);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedReviewValidationException))), Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.InsertReviewAsync(It.IsAny<Review>()), Times.Never);

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}

		[Theory]
#pragma warning disable xUnit1019 // MemberData must reference a member providing a valid data type
		[MemberData(nameof(InvalidStars))]
#pragma warning restore xUnit1019 // MemberData must reference a member providing a valid data type
		public async Task ShouldThrowValidationExceptionIfStarsAreOutOfRangeAndLogItAsync(
	   int invalidStars)
		{
			// given
			Review randomReview = CreateRandomReview(invalidStars);
			Review invalidReview = randomReview;
			var invalidReviewException = new InvalidReviewException();

			invalidReviewException.AddData(
				key: nameof(Review.Stars),
				values: "Stars are out of range");

			var expectedReviewValidationException =
				new ReviewValidationException(invalidReviewException);

			// when
			ValueTask<Review> addReviewTask = this.reviewService.AddReviewAsync(invalidReview);

			ReviewValidationException actualReviewValidationException =
				await Assert.ThrowsAsync<ReviewValidationException>(addReviewTask.AsTask);

			// then
			actualReviewValidationException.Should().
				BeEquivalentTo(expectedReviewValidationException);

			this.loggingBrokerMock.Verify(broker => broker.LogError(It.Is(
				SameExceptionAs(expectedReviewValidationException))), Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.InsertReviewAsync(It.IsAny<Review>()), Times.Never);

			this.dateTimeBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}
	}
}
