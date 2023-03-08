
using System.Threading.Tasks;
using CashOverflowUz.Models.job;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Jobs
{
    public partial class JobServiceTasts
    {
        [Fact]
        public async Task ShouldAddJonasync()
        {
            // given 
            Job randomJob = CreateRandomJob();
            Job inputJob = randomJob;
            Job persistedJob = inputJob;
            Job expectedJob = persistedJob.DeepClone();

            this.JobStorageBrokerMock.Setup(broker =>
            broker.InsertJobAsync(inputJob)).ReturnsAsync(inputJob);

            // when
            Job actualJob  = await this.jobService.AddJobAsync(inputJob);
            
            // then
            actualJob.Should().BeEquivalentTo(expectedJob);

            this.JobStorageBrokerMock.Verify(broker =>
            broker.InsertJobAsync(inputJob), Times.Once);

            this.JobStorageBrokerMock.VerifyNoOtherCalls();
        }

       
    }
}
