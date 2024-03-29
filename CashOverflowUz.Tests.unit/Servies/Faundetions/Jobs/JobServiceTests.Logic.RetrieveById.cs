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
		public async Task ShouldRetrieveJobByIdAsync()
		{
			//given
			Guid randomJobId = Guid.NewGuid();
			Guid inputJobId = randomJobId;
			Job randomJob = CreateRandomJob();
			Job storageJob = randomJob;
			Job excpectedJob = randomJob.DeepClone();

			this.storageBrokerMock.Setup(broker =>
				broker.SelectJobByIdAsync(inputJobId)).ReturnsAsync(storageJob);

			//when
			Job actuallJob = await this.jobService.RetrieveJobByIdAsync(inputJobId);

			//then
			actuallJob.Should().BeEquivalentTo(excpectedJob);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectJobByIdAsync(inputJobId), Times.Once());

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
