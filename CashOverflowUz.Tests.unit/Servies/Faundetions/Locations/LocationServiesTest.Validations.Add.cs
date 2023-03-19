using System;
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
            broker.LogError(It.Is(SameExceptionAs(expectedValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertLocationAysnc(It.IsAny<Location>()), Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]

        public async Task ShouldThrowValidationExceptionOnAddIfLocationIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given 

            var invalidLocation = new Location
            {
                Name = invalidText
            };

            var invalidLocationException = new InvalidLocationException();

            invalidLocationException.AddData(
                key: nameof(Location.id),
                values: "Id is required");

            invalidLocationException.AddData(
              key: nameof(Location.Name),
              values: "Text is required");

            invalidLocationException.AddData(
              key: nameof(Location.CreatedDate),
              values: "Date is required");

            invalidLocationException.AddData(
              key: nameof(Location.UpdatedDate),
              values: "Date is required");

            var expectedValidationException =
                new LocationValidationException(invalidLocationException);

            // when 

            ValueTask<Location> addLocationTask =
                this.locationService.AddLocationAsyncs(invalidLocation);

            LocationValidationException actualLocationValidationException =
                await Assert.ThrowsAsync<LocationValidationException>(addLocationTask.AsTask);

            // then

            actualLocationValidationException.Should()
                .BeEquivalentTo(expectedValidationException);

            this.loggingBrokerMock.Verify(broker =>
             broker.LogError(It.Is(SameExceptionAs(
               expectedValidationException))), Times.Once());

            this.storageBrokerMock.Verify(broker =>
            broker.InsertLocationAysnc(It.IsAny<Location>()), Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]

        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDatelsNotSameAsUpdateAndLogItAsync()
        {
            //  given 

            int randomMinutes = GetRandomNumber();
            DateTimeOffset ranDomDate = GetRandomDateTimeOffset();
            Location randomLocation = CreateRandomLocation(ranDomDate);
            Location invalidLocation = randomLocation;
            invalidLocation.UpdatedDate = ranDomDate.AddMinutes(randomMinutes);
            var invalidLocationException = new InvalidLocationException();

            invalidLocationException.AddData(
                key: nameof(Location.CreatedDate),
                values: $"Date is not same as {nameof(Location.UpdatedDate)}");

             var expectedlocationValidationException = 
                new LocationValidationException(invalidLocationException);

            // when 

            ValueTask<Location> addLocationTask = 
                this.locationService.AddLocationAsyncs(invalidLocation);

            LocationValidationException actualLocationValidationException =
                await Assert.ThrowsAsync<LocationValidationException>(addLocationTask.AsTask);

            // then

            actualLocationValidationException.
                Should().BeEquivalentTo(expectedlocationValidationException);

            this.loggingBrokerMock.Verify(broker =>
             broker.LogError(It.Is(SameExceptionAs(
                expectedlocationValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertLocationAysnc(It.IsAny<Location>()), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
