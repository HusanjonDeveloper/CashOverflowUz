// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashOverflowUz.Models.job;
using CashOverflowUz.Models.jobs.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Jobs
{
	public partial class JobServiceTests
	{
		[Fact]
		public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
		{
			// given
			DateTimeOffset someDateTime = GetRandomDateTime();
			Job randomJob = CreateRandomJob();
			Job someJob = randomJob;
			Guid jobId = someJob.Id;
			SqlException sqlException = CreateSqlException();

			var failedJobStorageException =
				new FailedJobStorageException(sqlException);

			var expectedJobDependencyException =
				new JobDependencyException(failedJobStorageException);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset()).Throws(sqlException);

			// when
			ValueTask<Job> modifyJobTask =
				this.jobService.ModifyJobAsync(someJob);

			JobDependencyException actualJobDependencyException =
				await Assert.ThrowsAsync<JobDependencyException>(
					modifyJobTask.AsTask);

			// then
			actualJobDependencyException.Should().BeEquivalentTo(
				expectedJobDependencyException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectJobByIdAsync(jobId), Times.Never);

			this.storageBrokerMock.Verify(broker =>
				broker.UpdateJobAsync(someJob), Times.Never);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogCritical(It.Is(SameExceptionAs(
					expectedJobDependencyException))), Times.Once);

			this.dateTimeBrokerMock.Verify(broker =>
			   broker.GetCurrentDateTimeOffset(), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowDependencyExceptionOnModifyIfDatabaseUpdateExceptionOccursAndLogItAsync()
		{
			// given
			int minutesInPast = GetRandomNegativeNumber();
			DateTimeOffset randomDateTime = GetRandomDatetimeOffset();
			Job randomJob = CreateRandomJob(randomDateTime);
			Job someJob = randomJob;
			someJob.CreatedDate = someJob.CreatedDate.AddMinutes(minutesInPast);
			var databaseUpdateException = new DbUpdateException();

			var failedStorageJobException =
				new FailedJobStorageException(databaseUpdateException);

			var expectedJobDependencyException =
				new JobDependencyException(failedStorageJobException);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset())
					.Throws(databaseUpdateException);

			// when
			ValueTask<Job> modifyJobTask =
				this.jobService.ModifyJobAsync(someJob);

			JobDependencyException actualJobDependencyException =
				await Assert.ThrowsAsync<JobDependencyException>(
					modifyJobTask.AsTask);

			// then
			actualJobDependencyException.Should().BeEquivalentTo(expectedJobDependencyException);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedJobDependencyException))), Times.Once);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowDependencyValidationExceptionOnModifyIfDatabaseUpdateConcurrencyErrorOccursAndLogItAsync()
		{
			// given
			int minutesInPast = GetRandomNegativeNumber();
			DateTimeOffset randomDateTime = GetRandomDateTime();
			Job randomJob = CreateRandomJob(randomDateTime);
			Job someJob = randomJob;
			someJob.CreatedDate = randomDateTime.AddMinutes(minutesInPast);
			var databaseUpdateConcurrencyException = new DbUpdateConcurrencyException();

			var lockedJobException =
				new LockedJobException(databaseUpdateConcurrencyException);

			var expectedJobDependencyValidationException =
				new JobDependencyValidationException(lockedJobException);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset())
					.Throws(databaseUpdateConcurrencyException);

			// when
			ValueTask<Job> modifyJobTask =
				this.jobService.ModifyJobAsync(someJob);

			JobDependencyValidationException actualJobDependencyValidationException =
				await Assert.ThrowsAsync<JobDependencyValidationException>(modifyJobTask.AsTask);

			// then
			actualJobDependencyValidationException.Should()
				.BeEquivalentTo(expectedJobDependencyValidationException);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedJobDependencyValidationException))), Times.Once);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowServiceExceptionOnModifyIfDatabaseUpdateErrorOccursAndLogItAsync()
		{
			// given
			int minutesInPast = GetRandomNegativeNumber();
			var randomDateTime = GetRandomDateTime();
			Job randomJob = CreateRandomJob(randomDateTime);
			Job someJob = randomJob;
			someJob.CreatedDate = someJob.CreatedDate.AddMinutes(minutesInPast);
			var serviceException = new Exception();

			var failedJobServiceException =
				new FailedJobServiceException(serviceException);

			var expectedJobServiceException =
				new JobServiceException(failedJobServiceException);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset())
					.Throws(serviceException);

			// when
			ValueTask<Job> modifyJobTask =
				this.jobService.ModifyJobAsync(someJob);

			JobServiceException actualJobServiceException =
				await Assert.ThrowsAsync<JobServiceException>(
					modifyJobTask.AsTask);

			// then
			actualJobServiceException.Should().BeEquivalentTo(expectedJobServiceException);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedJobServiceException))), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}
	}
}
