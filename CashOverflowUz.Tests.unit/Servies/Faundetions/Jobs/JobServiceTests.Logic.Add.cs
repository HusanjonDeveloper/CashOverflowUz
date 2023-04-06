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
		public async Task ShouldAddJobAsync()
		{
			// given
			DateTimeOffset randomDate = GetRandomDatetimeOffset();
			Job randomJob = CreateRandomJob(randomDate);
			Job inputJob = randomJob;
			Job persistedJob = inputJob;
			Job expectedJob = persistedJob.DeepClone();

			this.dateTimeBrokerMock.Setup(broker =>
			broker.GetCurrentDateTimeOffset()).Returns(randomDate);

			this.storageBrokerMock.Setup(broker =>
				broker.InsertJobAsync(inputJob)).ReturnsAsync(persistedJob);

			// when
			Job actualJob = await this.jobService
				.AddJobAsync(inputJob);

			// then
			actualJob.Should().BeEquivalentTo(expectedJob);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.InsertJobAsync(inputJob), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}
	}
}
