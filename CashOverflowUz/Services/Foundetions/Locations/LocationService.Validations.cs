//-------------------------------------------------
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
        private static void ValidateLocationOnAdd(Location location)
        {
            ValidateLocationNotNull(location);

            Validate(
                (Rule: Islnvalid(location.id), Parameter: nameof(location.id)),
                (Rule: Islnvalid(location.Name), Parameter: nameof(location.Name)),
                (Rule: Islnvalid(location.CreatedDate), Parameter: nameof(location.CreatedDate)),
                (Rule: Islnvalid(location.UpdatedDate), Parameter: nameof(location.UpdatedDate)));
        }

        private static void ValidateLocationNotNull(Location location)
        {
            if (location is null)
            {
                throw new NullLocationException();
            }
        }

        private static dynamic Islnvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic Islnvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "text is required"
        };

        private static dynamic Islnvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validatios)
        {
            var invalidLocationException =
                new InvalidLocationException();

            foreach ((dynamic rule, string parameter) in validatios)
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
