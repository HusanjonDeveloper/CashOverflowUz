using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Locations
{
    public partial class LocationServiesTest
    {
        [Fact]

        public async Task ShouldAddLocationAsync()
        {
            // given
            Location randomLocation = CreateRandomLocation();
            Location inputLocation = randomLocation;
            Location persistedLocation = inputLocation;
            Location expectedLocation = persistedLocation.DeepClone();

            this.storageBrokerMock.Setup(broker =>
            broker.InsertLocationAysnc(inputLocation)).ReturnsAsync(persistedLocation);
            // when
          Location actualLocation  =
                await this.locationService.AddLocationAsyncs(inputLocation);

            //  then
           actualLocation.Should().BeEquivalentTo(expectedLocation);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertLocationAysnc(inputLocation),Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }

       
    }
}
