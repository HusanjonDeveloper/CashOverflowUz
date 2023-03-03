﻿using System;
using CashOverflowUz.Brokers.Storages;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Services.Foundetions.Locations;
using Moq;
using Tynamix.ObjectFiller;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Locations
{
    public partial class LocationServiesTest
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly ILocationService locationService;

       public LocationServiesTest()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.locationService = new LocationService(
                storageBroker: this.storageBrokerMock.Object);
        }

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
 