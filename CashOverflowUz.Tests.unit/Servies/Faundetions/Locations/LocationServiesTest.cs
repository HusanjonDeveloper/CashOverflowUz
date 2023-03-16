using System;
using System.Linq.Expressions;
using CashOverflowUz.Brokers.DateTimes;
using CashOverflowUz.Brokers.Loggings;
using CashOverflowUz.Brokers.Storages;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Services.Foundetions.Locations;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Locations
{
    public partial class LocationServiesTest
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILocationService locationService;

        public LocationServiesTest()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock= new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.locationService = new LocationService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }
        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);
        private DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private Location CreateRandomLocation() =>
            CreateLocationFiller(GetRandomDateTimeOffset()).Create();

        private Filler<Location> CreateLocationFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Location>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }


    }
}
