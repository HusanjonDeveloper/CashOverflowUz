﻿using System.Linq;
using System;
using System.Threading.Tasks;
using CashOverflowUz.Brokers.DateTimes;
using CashOverflowUz.Brokers.Loggings;
using CashOverflowUz.Brokers.Storages;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Services.Foundetions.Locations
{
	public partial class LocationService : ILocationService
	{
		private readonly IStorageBroker storageBroker;
		private readonly ILoggingBroker loggingBroker;
		private readonly IDateTimeBroker dateTimeBroker;

		public LocationService(
			IStorageBroker storageBroker,
			ILoggingBroker loggingBroker,
			IDateTimeBroker dateTimeBroker)

		{
			this.storageBroker = storageBroker;
			this.loggingBroker = loggingBroker;
			this.dateTimeBroker = dateTimeBroker;
		}

		public ValueTask<Location> AddLocationAsync(Location location) =>
			TryCatch(async () =>
			{
				ValidateLocationOnAdd(location);

				return await this.storageBroker.InsertLocationAsync(location);
			});

		public IQueryable<Location> RetrieveAllLocations() =>
			TryCatch(() => this.storageBroker.SelectAllLocations());

		public ValueTask<Location> RetrieveLocationByIdAsync(Guid locationId) =>
			TryCatch(async () =>
			{
				ValidateLocationId(locationId);

				Location maybeLocation =
					await this.storageBroker.SelectLocationByIdAsync(locationId);

				ValidateStorageLocation(maybeLocation, locationId);

				return maybeLocation;
			});

		public ValueTask<Location> ModifyLocationAsync(Location location) =>
		TryCatch(async () =>
		{
			ValidateLocationOnModify(location);
			var maybeLocation = await this.storageBroker.SelectLocationByIdAsync(location.Id);
			ValidateAgainstStorageLocationOnModify(inputLocation: location, storageLocation: maybeLocation);

			return await this.storageBroker.UpdateLocationAsync(location);
		});

		public ValueTask<Location> RemoveLocationByIdAsync(Guid locationId) =>
		TryCatch(async () =>
		{
			ValidateLocationById(locationId);

			Location someLocation =
				await this.storageBroker.SelectLocationByIdAsync(locationId);

			ValidateStorageLocation(someLocation, locationId);

			return await this.storageBroker.DeleteLocationAsync(someLocation);
		});
	}
}
