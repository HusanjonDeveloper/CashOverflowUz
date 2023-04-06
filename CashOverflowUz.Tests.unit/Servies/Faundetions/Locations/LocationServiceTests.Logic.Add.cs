using System;
using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Locations
{
	public partial class LocationServiceTests
	{
		[Fact]
		public async Task ShouldAddLocationAsync()
		{
			// given
			DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
			Location randomLocation = CreateRandomLocation(randomDateTime);
			Location inputLocation = randomLocation;
			Location persistedLocation = inputLocation;
			Location expectedLocation = persistedLocation.DeepClone();

			this.dateTimeBrokerMock.Setup(broker => broker.GetCurrentDateTimeOffset())
				.Returns(randomDateTime);

			this.storageBrokerMock.Setup(broker =>
				broker.InsertLocationAysnc(inputLocation)).ReturnsAsync(persistedLocation);

			// when
			Location actualLocation = await this.locationService
				.AddLocationAsyncs(inputLocation);

			// then
			actualLocation.Should().BeEquivalentTo(expectedLocation);

			this.dateTimeBrokerMock.Verify(broker => broker
				.GetCurrentDateTimeOffset(), Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.InsertLocationAysnc(inputLocation), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}
	}
}
