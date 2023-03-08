using System;
using CashOverflowUz.Brokers.Storages;
using CashOverflowUz.Models.job;
using CashOverflowUz.Services.Foundetions.Jobs;
using Moq;
using Tynamix.ObjectFiller;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Jobs
{
    public partial class JobServiceTasts
    {
        private readonly Mock<IStorageBroker> JobStorageBrokerMock;
        private readonly IJobService jobService;

        public JobServiceTasts()
        {
            this.JobStorageBrokerMock= new Mock<IStorageBroker>();
            this.jobService = new JobService(
                storageBroker: this.JobStorageBrokerMock.Object);
        }
        private DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();
        private Job CreateRandomJob() =>
             CreateJobFiller(GetRandomDateTimeOffset()).Create();

        private Filler<Job> CreateJobFiller(DateTimeOffset dates)
        { 
          var filler = new Filler<Job>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }
    }
}
