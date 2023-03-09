using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Models.Locations.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Locations
{
    public partial class LocationServiesTest
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIFInputIsNullAndLogItAsync()
        {
            // given 
            Location nullLocation = null;
            var nullLocationException = new NullLocationException();

            var expectedValidationException =
                new LocationValidationException(nullLocationException);
            // when 
            ValueTask<Location> addLocationTask = this.locationService
                .AddLocationAsyncs(nullLocation);

            LocationValidationException actualLocationValidationException =
                await Assert.ThrowsAsync<LocationValidationException>(addLocationTask.AsTask);
            // then 

            actualLocationValidationException.Should()
                .BeEquivalentTo(expectedValidationException);

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(expectedValidationException))),Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertLocationAysnc(It.IsAny<Location>()),Times.Never);

             this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
