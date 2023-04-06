﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Models.Locations.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Locations
{
	public partial class LocationServiceTests
	{
		[Fact]
		public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
		{
			// given
			DateTimeOffset someDateTime = GetRandomDateTime();
			Location randomLocation = CreateRandomLocation(someDateTime);
			Location someLocation = randomLocation;
			Guid LocationId = someLocation.id;
			SqlException sqlException = CreateSqlException();

			var failedLocationStorageException =
				new FailedLocationStorageException(sqlException);

			var expectedLocationDependencyException =
				new LocationDependencyException(failedLocationStorageException);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset()).Throws(sqlException);

			// when
			ValueTask<Location> modifyLocationTask =
				this.locationService.ModifyLocationAsync(someLocation);

			LocationDependencyException actualLocationDependencyException =
				await Assert.ThrowsAsync<LocationDependencyException>(
					 modifyLocationTask.AsTask);

			// then
			actualLocationDependencyException.Should().BeEquivalentTo(
				expectedLocationDependencyException);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogCritical(It.Is(SameExceptionAs(
					expectedLocationDependencyException))), Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLocationByIdAsync(LocationId), Times.Never);

			this.storageBrokerMock.Verify(broker =>
				broker.UpdateLocationAsync(someLocation), Times.Never);

			this.dateTimeBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowDependencyExceptionOnModifyIfDatabaseUpdateExceptionOccursAndLogItAsync()
		{
			// given
			int minutesInPast = GetRandomNegativeNumber();
			DateTimeOffset randomDateTime = GetRandomDateTime();
			Location randomLocation = CreateRandomLocation(randomDateTime);
			Location someLocation = randomLocation;
			Guid LocationId = someLocation.id;
			someLocation.CreatedDate = randomDateTime.AddMinutes(minutesInPast);
			var databaseUpdateException = new DbUpdateException();

			var failedLocationException =
				new FailedLocationStorageException(databaseUpdateException);

			var expectedLocationDependencyException =
				new LocationDependencyException(failedLocationException);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset())
					.Throws(databaseUpdateException);

			// when
			ValueTask<Location> modifyLocationTask =
				this.locationService.ModifyLocationAsync(someLocation);

			LocationDependencyException actualLocationDependencyException =
				 await Assert.ThrowsAsync<LocationDependencyException>(
					 modifyLocationTask.AsTask);

			// then
			actualLocationDependencyException.Should().BeEquivalentTo(
				expectedLocationDependencyException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLocationByIdAsync(LocationId), Times.Never);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedLocationDependencyException))), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowDependencyValidationExceptionOnModifyIfDatabaseUpdateConcurrencyErrorOccursAndLogItAsync()
		{
			// given
			int minutesInPast = GetRandomNegativeNumber();
			DateTimeOffset randomDateTime = GetRandomDateTime();
			Location randomLocation = CreateRandomLocation(randomDateTime);
			Location someLocation = randomLocation;
			someLocation.CreatedDate = randomDateTime.AddMinutes(minutesInPast);
			Guid LocationId = someLocation.id;
			var databaseUpdateConcurrencyException = new DbUpdateConcurrencyException();

			var lockedLocationException =
				new LockedLocationException(databaseUpdateConcurrencyException);

			var expectedLocationDependencyValidationException =
				new LocationDependencyValidationException(lockedLocationException);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset())
					.Throws(databaseUpdateConcurrencyException);

			// when
			ValueTask<Location> modifyLocationTask =
				this.locationService.ModifyLocationAsync(someLocation);

			LocationDependencyValidationException actualLocationDependencyValidationException =
				await Assert.ThrowsAsync<LocationDependencyValidationException>(
					modifyLocationTask.AsTask);

			// then
			actualLocationDependencyValidationException.Should().BeEquivalentTo(
				expectedLocationDependencyValidationException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLocationByIdAsync(LocationId), Times.Never);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedLocationDependencyValidationException))), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowServiceExceptionOnModifyIfDatabaseUpdateErrorOccursAndLogItAsync()
		{
			// given
			int minuteInPast = GetRandomNegativeNumber();
			DateTimeOffset randomDateTime = GetRandomDateTime();
			Location randomLocation = CreateRandomLocation(randomDateTime);
			Location someLocation = randomLocation;
			someLocation.CreatedDate = randomDateTime.AddMinutes(minuteInPast);
			var serviceException = new Exception();

			var failedLocationException =
				new FailedLocationServiceException(serviceException);

			var expectedLocationServiceException =
				new LocationServiceException(failedLocationException);

			this.dateTimeBrokerMock.Setup(broker =>
				broker.GetCurrentDateTimeOffset())
					.Throws(serviceException);

			// when
			ValueTask<Location> modifyLocationTask =
				this.locationService.ModifyLocationAsync(someLocation);

			LocationServiceException actualLocationServiceException =
				await Assert.ThrowsAsync<LocationServiceException>(
					modifyLocationTask.AsTask);

			// then
			actualLocationServiceException.Should().BeEquivalentTo(
				expectedLocationServiceException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLocationByIdAsync(someLocation.id), Times.Never);

			this.dateTimeBrokerMock.Verify(broker =>
				broker.GetCurrentDateTimeOffset(), Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedLocationServiceException))), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
