﻿//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Models.Locations.Exceptions;

namespace CashOverflowUz.Services.Foundetions.Locations
{
	public partial class LocationService
	{

		private void ValidateLocationOnAdd(Location location)
		{
			ValidateLocationNotNull(location);

			Validate(
				(Rule: IsInvalid(location.Id), Parameter: nameof(Location.Id)),
				(Rule: IsInvalid(location.Name), Parameter: nameof(Location.Name)),
				(Rule: IsInvalid(location.CreatedDate), Parameter: nameof(Location.CreatedDate)),
				(Rule: IsInvalid(location.UpdatedDate), Parameter: nameof(Location.UpdatedDate)),
				(Rule: IsNotRecent(location.CreatedDate), Parameter: nameof(Location.CreatedDate)),

				(Rule: IsInvalid(
					firstDate: location.CreatedDate,
					secondDate: location.UpdatedDate,
					secondDateName: nameof(Location.UpdatedDate)),

				 Parameter: nameof(Location.CreatedDate)));
		}

		private void ValidateLocationOnModify(Location location)
		{
			ValidateLocationNotNull(location);

			Validate(
				(Rule: IsInvalid(location.Id), Parameter: nameof(Location.Id)),
				(Rule: IsInvalid(location.Name), Parameter: nameof(Location.Name)),
				(Rule: IsInvalid(location.CreatedDate), Parameter: nameof(Location.CreatedDate)),
				(Rule: IsInvalid(location.UpdatedDate), Parameter: nameof(Location.UpdatedDate)),
				(Rule: IsNotRecent(location.UpdatedDate), Parameter: nameof(Location.UpdatedDate)),

				(Rule: IsSame(
					firstDate: location.UpdatedDate,
					secondDate: location.CreatedDate,
					secondDateName: nameof(location.CreatedDate)),
					Parameter: nameof(location.UpdatedDate)));
		}

		private static void ValidateAgainstStorageLocationOnModify(Location inputLocation, Location storageLocation)
		{
			ValidateStorageLocation(storageLocation, inputLocation.Id);

			Validate(
			(Rule: IsNotSame(
					firstDate: inputLocation.CreatedDate,
					secondDate: storageLocation.CreatedDate,
					secondDateName: nameof(Location.CreatedDate)),
					Parameter: nameof(Location.CreatedDate)),

			(Rule: IsSame(
					firstDate: inputLocation.UpdatedDate,
					secondDate: storageLocation.UpdatedDate,
					secondDateName: nameof(Location.UpdatedDate)),
					Parameter: nameof(Location.UpdatedDate)));
		}

		private static dynamic IsNotSame(
			DateTimeOffset firstDate,
			DateTimeOffset secondDate,
			string secondDateName) => new
			{
				Condition = firstDate != secondDate,
				Message = $"Date is not same as {secondDateName}"
			};

		private static dynamic IsSame(
			DateTimeOffset firstDate,
			DateTimeOffset secondDate,
			string secondDateName) => new
			{
				Condition = firstDate == secondDate,
				Message = $"Date is the same as {secondDateName}"
			};

		private static void ValidateLocationId(Guid locationId) =>
				Validate((Rule: IsInvalid(locationId), Parameter: nameof(Location.Id)));

		private static void ValidateStorageLocation(Location maybeLocation, Guid locationId)
		{
			if (maybeLocation is null)
			{
				throw new NotFoundLocationException(locationId);
			}
		}

		private static void ValidateLocationNotNull(Location location)
		{
			if (location is null)
			{
				throw new NullLocationException();
			}
		}

		public void ValidateLocationById(Guid locationId) =>
		   Validate((Rule: IsInvalid(locationId), Parameter: nameof(Location.Id)));

		private static dynamic IsInvalid(Guid id) => new
		{
			Condition = id == default,
			Message = "Id is required"
		};

		private static dynamic IsInvalid(string text) => new
		{
			Condition = String.IsNullOrWhiteSpace(text),
			Message = "Text is required"
		};

		private static dynamic IsInvalid(
			DateTimeOffset firstDate,
			DateTimeOffset secondDate,
			string secondDateName) => new
			{
				Condition = firstDate != secondDate,
				Message = $"Date is not same as {secondDateName}"
			};

		private static dynamic IsInvalid(DateTimeOffset date) => new
		{
			Condition = date == default,
			Message = "Date is required"
		};

		private dynamic IsNotRecent(DateTimeOffset date) => new
		{
			Condition = IsDateNotRecent(date),
			Message = "Date is not recent"
		};

		private bool IsDateNotRecent(DateTimeOffset date)
		{
			DateTimeOffset currentDate = this.dateTimeBroker.GetCurrentDateTimeOffset();
			TimeSpan timeDifference = currentDate.Subtract(date);

			return timeDifference.TotalSeconds is > 60 or < 0;
		}

		private static void Validate(params (dynamic Rule, string Parameter)[] validations)
		{
			var invalidLocationException = new InvalidLocationException();

			foreach ((dynamic rule, string parameter) in validations)
			{
				if (rule.Condition)
				{
					invalidLocationException.UpsertDataList(
						key: parameter,
						value: rule.Message);
				}
			}

			invalidLocationException.ThrowIfContainsErrors();
		}
	}
}
