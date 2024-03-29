// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using CashOverflowUz.Models.Locations.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Locations
{
	public partial class LocationServiceTests
	{
		[Fact]
		public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
		{
			// given
			SqlException sqlException = CreateSqlException();

			var failedStorageException =
				new FailedLocationStorageException(sqlException);

			var expectedLocationDependencyException =
				new LocationDependencyException(failedStorageException);

			this.storageBrokerMock.Setup(broker =>
				broker.SelectAllLocations())
					.Throws(sqlException);

			// when
			Action retrieveAllLocationsAction = () =>
			   this.locationService.RetrieveAllLocations();

			LocationDependencyException actualLocationDependencyException =
				Assert.Throws<LocationDependencyException>(
					retrieveAllLocationsAction);

			// then
			actualLocationDependencyException.Should().BeEquivalentTo(
			   expectedLocationDependencyException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectAllLocations(),
					Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogCritical(It.Is(SameExceptionAs(
					expectedLocationDependencyException))),
						Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		private void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
		{
			// given
			string exceptionMessage = GetRandomString();
			var serviceException = new Exception(exceptionMessage);

			var failedLocationServiceException =
				new FailedLocationServiceException(serviceException);

			var expectedLocationServiceException =
				new LocationServiceException(failedLocationServiceException);

			this.storageBrokerMock.Setup(broker =>
				broker.SelectAllLocations())
					.Throws(serviceException);

			// when
			Action retrieveAllLocationsActions = () =>
				this.locationService.RetrieveAllLocations();

			LocationServiceException actualLocationServiceException =
				Assert.Throws<LocationServiceException>(
					retrieveAllLocationsActions);

			// then
			actualLocationServiceException.Should().BeEquivalentTo(expectedLocationServiceException);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectAllLocations(),
					Times.Once);

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedLocationServiceException))),
						Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}
	}
}
