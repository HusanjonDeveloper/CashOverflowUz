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
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Jobs
{
	public partial class JobServiceTests
	{
		[Fact]
		public void ShouldRetrieveAllJobs()
		{
			//given
			IQueryable<Job> randomJobs = CreateRandomJobs();
			IQueryable<Job> storageJobs = randomJobs;
			IQueryable<Job> expectedJobs = storageJobs.DeepClone();

			this.storageBrokerMock.Setup(broker =>
				broker.SelectAllJobs()).Returns((Delegate)storageJobs);

			//when
			IQueryable<Job> actualJobs =
				this.jobService.RetrieveAllJobs();

			//then
			actualJobs.Should().BeEquivalentTo(expectedJobs);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectAllJobs(), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
