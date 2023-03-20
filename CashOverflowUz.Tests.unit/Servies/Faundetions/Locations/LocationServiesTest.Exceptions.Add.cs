using System;
using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Models.Locations.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Locations
{
    public partial class LocationServiesTest
    {
        [Fact]
        public async Task ShouldThrowCriticalDpendencyExceptionOnAddIfDepdencyErrorOccursAndLogItAsync()
        {
            // given 
            Location someLocation = CreateRandomLocation();
            SqlException sqlException = CreateSqlException();
            var  failedLocationStorageException = new FailedLocationStorageException(sqlException);
            var expectedlocationDependencyException = new LocationDependencyException(failedLocationStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
            broker.GetCurrentDateTimesOffset()).Throws(sqlException);

            // when

            ValueTask<Location> addLocationTask = this.locationService.AddLocationAsyncs(someLocation);

            LocationDependencyException actualLocationDependencyException =
                await Assert.ThrowsAsync<LocationDependencyException>(addLocationTask.AsTask);

            // then 
            actualLocationDependencyException.Should().BeEquivalentTo(expectedlocationDependencyException);
            
            this.dateTimeBrokerMock.Verify(broker =>
            broker.GetCurrentDateTimesOffset(),Times.Once);

            this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(expectedlocationDependencyException))),
             Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        
        }
    }
}
