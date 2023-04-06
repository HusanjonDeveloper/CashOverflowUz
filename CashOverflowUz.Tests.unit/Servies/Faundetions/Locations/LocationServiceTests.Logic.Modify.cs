﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

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
		public async Task ShouldModifyLocationAsync()
		{
			//given
			DateTimeOffset randomDate = GetRandomDateTimeOffset();
			Location randomLocation = CreateRandomModifyLocation(randomDate);
			Location inputLocation = randomLocation;
			Location storageLocation = inputLocation.DeepClone();
			storageLocation.UpdatedDate = randomLocation.CreatedDate;
			Location updatedLocation = inputLocation;
			Location expectedLocation = updatedLocation.DeepClone();
			Guid LocationId = inputLocation.id;

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset()).Returns(randomDate);

			this.storageBrokerMock.Setup(broker =>
				broker.SelectLocationByIdAsync(LocationId))
					.ReturnsAsync(storageLocation);

			this.storageBrokerMock.Setup(broker =>
				broker.UpdateLocationAsync(inputLocation))
					.ReturnsAsync(updatedLocation);

			//when
			Location actualLocation =
				await this.locationService.ModifyLocationAsync(inputLocation);

			//then
			actualLocation.Should().BeEquivalentTo(expectedLocation);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLocationByIdAsync(LocationId), Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.UpdateLocationAsync(inputLocation), Times.Once);

			this.dateTimeBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
