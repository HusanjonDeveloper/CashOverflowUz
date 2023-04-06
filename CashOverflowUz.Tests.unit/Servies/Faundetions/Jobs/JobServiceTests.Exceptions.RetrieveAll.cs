// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashOverflowUz.Models.jobs.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Jobs
{
	public partial class JobServiceTests
	{
		[Fact]
		public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
		{
			//given
			SqlException sqlException = CreateSqlException();

			var faildeStorageException =
				new FailedJobStorageException(sqlException);

			var expectedJobDependencyException =
				new JobDependencyException(faildeStorageException);

			this.storageBrokerMock.Setup(broker =>
				broker.SelectAllJobs()).Throws(sqlException);

			//when
			Action retrieveAllJobsAction = () =>
				this.jobService.RetrieveAllJobs();

			JobDependencyException actualJobDependencyException =
				Assert.Throws<JobDependencyException>(retrieveAllJobsAction);

			//then
			actualJobDependencyException.Should().BeEquivalentTo(
				expectedJobDependencyException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectAllJobs(),
					Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogCritical(It.Is(SameExceptionAs(
					expectedJobDependencyException))), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
		{
			// given
			string exceptionMessage = GetRandomString();
			var serviceException = new Exception(exceptionMessage);

			var failedJobServiceException =
				new FailedJobServiceException(serviceException);

			var expectedJobServiceException =
				new JobServiceException(failedJobServiceException);

			this.storageBrokerMock.Setup(broker =>
				broker.SelectAllJobs()).Throws(serviceException);

			// when
			Action retrieveAllJobsAction = () =>
				this.jobService.RetrieveAllJobs();

			JobServiceException actualJobServiceException =
				Assert.Throws<JobServiceException>(retrieveAllJobsAction);

			// then
			actualJobServiceException.Should().BeEquivalentTo(expectedJobServiceException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectAllJobs(),
					Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedJobServiceException))),
						Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
