// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Threading.Tasks;
using CashOverflowUz.Models.Languages;
using CashOverflowUz.Models.Languages.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Languages
{
	public partial class LanguageServiceTests
	{
		[Fact]
		public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdAsyncIfSqlErrorOccursAndLogItAsync()
		{
			//given
			Guid someId = Guid.NewGuid();
			SqlException sqlException = CreateSqlException();

			var failedLanguageStorageException =
				new FailedLanguageStorageException(sqlException);

			LanguageDependencyException expectedLanguageDependencyException =
				new LanguageDependencyException(failedLanguageStorageException);

			this.storageBrokerMock.Setup(broker =>
				broker.SelectLanguageByIdAsync(It.IsAny<Guid>())).ThrowsAsync(sqlException);

			//when
			ValueTask<Language> retrieveLanguageByIdTask =
				this.languageService.RetrieveLanguageByIdAsync(someId);

			LanguageDependencyException actualLanguageDependencyException =
				await Assert.ThrowsAsync<LanguageDependencyException>(retrieveLanguageByIdTask.AsTask);

			//then
			actualLanguageDependencyException.Should().BeEquivalentTo(expectedLanguageDependencyException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLanguageByIdAsync(It.IsAny<Guid>()), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogCritical(It.Is(SameExceptionAs(
					expectedLanguageDependencyException))), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowServiceExceptionOnRetrieveByIdAsyncIfServiceErrorOccursAndLogItAsync()
		{
			//given
			Guid someId = Guid.NewGuid();
			var serviceException = new Exception();

			var failedLanguageServiceException =
				new FailedLanguageServiceException(serviceException);

			var expectedLanguageServiceExcpetion =
				new LanguageServiceException(failedLanguageServiceException);

			this.storageBrokerMock.Setup(broker =>
				broker.SelectLanguageByIdAsync(It.IsAny<Guid>())).ThrowsAsync(serviceException);

			//when
			ValueTask<Language> retrieveLanguageById =
				this.languageService.RetrieveLanguageByIdAsync(someId);

			LanguageServiceException actualLanguageServiceException =
				await Assert.ThrowsAsync<LanguageServiceException>(retrieveLanguageById.AsTask);

			// then
			actualLanguageServiceException.Should().BeEquivalentTo(expectedLanguageServiceExcpetion);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLanguageByIdAsync(It.IsAny<Guid>()), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
			   broker.LogError(It.Is(SameExceptionAs(
				   expectedLanguageServiceExcpetion))), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}
	}
}
