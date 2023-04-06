// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Threading.Tasks;
using CashOverflowUz.Models.Languages;
using CashOverflowUz.Models.Languages.Exceptions;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Languages
{
	public partial class LanguageServiceTests
	{
		[Fact]
		public async Task ShouldThrowValidationExceptionOnModifyIfLanguageIsNullAndLogItAsync()
		{
			//given
			Language nullLanguage = null;
			var nullLanguageException = new NullLanguageException();

			var excpectedLanguageValidationException =
				new LanguageValidationException(nullLanguageException);

			//when
			ValueTask<Language> modifyLanguageTask =
				this.languageService.ModifyLanguageAsync(nullLanguage);

			LanguageValidationException actualLanguageValidationException =
				await Assert.ThrowsAsync<LanguageValidationException>(modifyLanguageTask.AsTask);

			//then
			actualLanguageValidationException.Should().BeEquivalentTo(excpectedLanguageValidationException);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(excpectedLanguageValidationException))), Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.UpdateLanguageAsync(It.IsAny<Language>()), Times.Never);

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}

		[Theory]
		[InlineData(null)]
		[InlineData(" ")]
		[InlineData("  ")]
		public async Task ShouldThrowValidationExceptionOnModifyIfLanguageIsInvalidAndLogItAsync(
			string invalidString)
		{
			//given
			var invalidLanguage = new Language
			{
				Name = invalidString
			};

			var invalidLanguageException = new InvalidLanguageException();

			invalidLanguageException.AddData(
				key: nameof(Language.Id),
				values: "Id is required");

			invalidLanguageException.AddData(
				key: nameof(Language.Name),
				values: "Text is required");

			invalidLanguageException.AddData(
				key: nameof(Language.CreatedDate),
				values: "Date is required");

			invalidLanguageException.AddData(
				key: nameof(Language.UpdatedDate),
				"Date is required",
				"Date is not recent",
				$"Date is the same as {nameof(Language.CreatedDate)}");

			var excpectedLanguageValidationException =
				new LanguageValidationException(invalidLanguageException);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset()).Returns(GetRandomDatetimeOffset);

			//when
			ValueTask<Language> modifyLanguageTask = this.languageService.ModifyLanguageAsync(invalidLanguage);

			LanguageValidationException actualLanguageValidationException =
				await Assert.ThrowsAsync<LanguageValidationException>(modifyLanguageTask.AsTask);

			//then
			actualLanguageValidationException.Should().BeEquivalentTo(excpectedLanguageValidationException);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(excpectedLanguageValidationException))), Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.UpdateLanguageAsync(It.IsAny<Language>()), Times.Never);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowValidationExceptionOnModifyIfUpdatedDateIsNotSameAsCreatedDateAndLogItAsync()
		{
			//given
			DateTimeOffset randomDateTime = GetRandomDatetimeOffset();
			Language randomLanguage = CreateRandomLanguage(randomDateTime);
			Language invalidLanguage = randomLanguage;
			var invalidLanguageException = new InvalidLanguageException();

			invalidLanguageException.AddData(
				key: nameof(Language.UpdatedDate),
				values: $"Date is the same as {nameof(Language.CreatedDate)}");

			var expectedLanguageValidationException =
				new LanguageValidationException(invalidLanguageException);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset()).Returns(randomDateTime);

			//when
			ValueTask<Language> modifyLanguageTask = this.languageService.ModifyLanguageAsync(invalidLanguage);

			LanguageValidationException actualLanguageValidationException =
				await Assert.ThrowsAsync<LanguageValidationException>(modifyLanguageTask.AsTask);

			//then
			actualLanguageValidationException.Should().BeEquivalentTo(
				expectedLanguageValidationException);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(expectedLanguageValidationException))), Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLanguageByIdAsync(invalidLanguage.Id), Times.Never);

			this.dateTimeBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}

		[Theory]
		[MemberData(nameof(InvalidSeconds))]
		public async Task ShouldThrowValidationExceptionOnModifyIfUpdatedDateIsNotRecentAndLogItAsync(
			int invalidMinutes)
		{
			//given
			DateTimeOffset randomDateTime = GetRandomDatetimeOffset();
			Language randomLanguage = CreateRandomLanguage(randomDateTime);
			Language inputLanguage = randomLanguage;
			inputLanguage.UpdatedDate = randomDateTime.AddSeconds(invalidMinutes);
			var invalidLanguageException = new InvalidLanguageException();

			invalidLanguageException.AddData(
				key: nameof(Language.UpdatedDate),
				values: "Date is not recent");

			var expectedLanguageValidationException =
				new LanguageValidationException(invalidLanguageException);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset()).Returns(randomDateTime);

			//when
			ValueTask<Language> modifyLanguageTask = this.languageService.ModifyLanguageAsync(inputLanguage);

			LanguageValidationException actualLanguageValidationException =
				await Assert.ThrowsAsync<LanguageValidationException>(modifyLanguageTask.AsTask);

			//then
			actualLanguageValidationException.Should().BeEquivalentTo(
				expectedLanguageValidationException);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(expectedLanguageValidationException))), Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLanguageByIdAsync(It.IsAny<Guid>()), Times.Never);

			this.dateTimeBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowValidationExceptionOnModifyIfLanguageDoesNotExistAndLogItAsync()
		{
			//given 
			int randomNegativeMinutes = GetRandomNegativeNumber();
			DateTimeOffset dateTime = GetRandomDatetimeOffset();
			Language randomLanguage = CreateRandomLanguage(dateTime);
			Language nonExistLanguage = randomLanguage;
			nonExistLanguage.CreatedDate = dateTime.AddMinutes(randomNegativeMinutes);
			Language nullLanguage = null;

			var notFoundLanguageException =
				new NotFoundLanguageException(nonExistLanguage.Id);

			var expectedLanguageValidationException =
				new LanguageValidationException(notFoundLanguageException);

			this.storageBrokerMock.Setup(broker =>
				broker.SelectLanguageByIdAsync(nonExistLanguage.Id)).ReturnsAsync(nullLanguage);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset()).Returns(dateTime);

			//when 
			ValueTask<Language> modifyLanguageTask =
				this.languageService.ModifyLanguageAsync(nonExistLanguage);

			LanguageValidationException actualLanguageValidationException =
			   await Assert.ThrowsAsync<LanguageValidationException>(modifyLanguageTask.AsTask);

			//then
			actualLanguageValidationException.Should().BeEquivalentTo(expectedLanguageValidationException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLanguageByIdAsync(nonExistLanguage.Id), Times.Once);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedLanguageValidationException))), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowValidationExceptionOnModifyIfStorageCreatedDateNotSameAsCreatedDateAndLogItAsync()
		{
			//given 
			int randomNumber = GetRandomNegativeNumber();
			int randomMinutes = randomNumber;
			DateTimeOffset randomDateTime = GetRandomDatetimeOffset();
			Language randomLanguage = CreateRandomModifyLanguage(randomDateTime);
			Language invalidLanguage = randomLanguage.DeepClone();
			Language storageLanguage = invalidLanguage.DeepClone();
			storageLanguage.CreatedDate = storageLanguage.CreatedDate.AddMinutes(randomMinutes);
			storageLanguage.UpdatedDate = storageLanguage.UpdatedDate.AddMinutes(randomMinutes);
			var invalidLanguageException = new InvalidLanguageException();
			Guid LanguageId = invalidLanguage.Id;

			invalidLanguageException.AddData(
				key: nameof(Language.CreatedDate),
				values: $"Date is not same as {nameof(Language.CreatedDate)}");

			var expectedLanguageValidationException =
				new LanguageValidationException(invalidLanguageException);

			this.storageBrokerMock.Setup(broker =>
				broker.SelectLanguageByIdAsync(LanguageId)).ReturnsAsync(storageLanguage);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset()).Returns(randomDateTime);

			//when 
			ValueTask<Language> modifyLanguageTask =
			   this.languageService.ModifyLanguageAsync(invalidLanguage);

			LanguageValidationException actualLanguageValidationException =
				await Assert.ThrowsAsync<LanguageValidationException>(modifyLanguageTask.AsTask);

			//then
			actualLanguageValidationException.Should().BeEquivalentTo(expectedLanguageValidationException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLanguageByIdAsync(invalidLanguage.Id), Times.Once);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
			   broker.LogError(It.Is(SameExceptionAs(
				   expectedLanguageValidationException))), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowValidationExceptionOnModifyIfStorageUpdatedDateSameAsUpdatedDateAndLogItAsync()
		{
			//given 
			DateTimeOffset randomDateTime = GetRandomDatetimeOffset();
			Language randomLanguage = CreateRandomModifyLanguage(randomDateTime);
			Language invalidLanguage = randomLanguage;
			Language storageLanguage = randomLanguage.DeepClone();
			invalidLanguage.UpdatedDate = storageLanguage.UpdatedDate;
			Guid LanguageId = invalidLanguage.Id;
			var invalidLanguageException = new InvalidLanguageException();

			invalidLanguageException.AddData(
			key: nameof(Language.UpdatedDate),
				values: $"Date is the same as {nameof(Language.UpdatedDate)}");

			var expectedLanguageValidationException =
				new LanguageValidationException(invalidLanguageException);

			this.storageBrokerMock.Setup(broker =>
			   broker.SelectLanguageByIdAsync(invalidLanguage.Id)).ReturnsAsync(storageLanguage);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset()).Returns(randomDateTime);

			//when 
			ValueTask<Language> modifyLanguageTask = this.languageService.ModifyLanguageAsync(invalidLanguage);

			LanguageValidationException actualLanguageValidationException =
			   await Assert.ThrowsAsync<LanguageValidationException>(modifyLanguageTask.AsTask);

			//then
			actualLanguageValidationException.Should().BeEquivalentTo(expectedLanguageValidationException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLanguageByIdAsync(LanguageId), Times.Once);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedLanguageValidationException))), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
