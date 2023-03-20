using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using CashOverflowUz.Brokers.Loggings;
using CashOverflowUz.Brokers.Storages;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Services.Foundetions.Locations;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

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
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.locationService = new LocationService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<int> InvalidMinutes()
        {
            int minutesInFuture = GetRandomNumber();
            int minutesInPast = GetRandomNegativeNumber();

            return new TheoryData<int>
            {
                minutesInFuture,
                minutesInPast
            };
        }


        private SqlException CreateSqlException() =>
         (SqlException)FormatterServices.GetSafeUninitializedObject(typeof(SqlException));   
        
        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);


        private static int GetRandomNegativeNumber() =>
          -1*  new IntRange(min: 2, max: 9).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();
        private DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();
        private Location CreateRandomLocation(DateTimeOffset dates) =>
           CreateLocationFiller(dates).Create();
        private Location CreateRandomLocation() =>
            CreateLocationFiller(dates:GetRandomDateTimeOffset()).Create();

        private Filler<Location> CreateLocationFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Location>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }


    }
}
