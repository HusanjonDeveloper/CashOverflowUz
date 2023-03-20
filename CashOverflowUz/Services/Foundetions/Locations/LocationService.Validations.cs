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
                (Rule: Islnvalid(location.id), Parameter: nameof(Location.id)),
                (Rule: Islnvalid(location.Name), Parameter: nameof(Location.Name)),
                (Rule: Islnvalid(location.CreatedDate), Parameter: nameof(Location.CreatedDate)),
                (Rule: Islnvalid(location.UpdatedDate), Parameter: nameof(Location.UpdatedDate)),
                (Rule: IsNotRecent(location.CreatedDate), Parameter: nameof(Location.CreatedDate)),

                (Rule: Islnvalid(
                    fristDate: location.CreatedDate,
                    secondDate: location.UpdatedDate,
                    secondDateName: nameof(Location.UpdatedDate)),

                    Parameter: nameof(Location.CreatedDate)));
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

        private static dynamic Islnvalid(
            DateTimeOffset fristDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = fristDate != secondDate,
                Message = $"Date is not same as {secondDateName}"
            };

        private static dynamic Islnvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private  dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };

        private  bool IsDateNotRecent(DateTimeOffset date)// 10:50:20
        {
          DateTimeOffset currentDate =this.dateTimebroker.GetCurrentDateOffset();// 10:51:00
          TimeSpan timeDifference = currentDate.Subtract(date);// 40
            double seconds = timeDifference.TotalSeconds;

            return timeDifference.Seconds is > 60 or < 0;
        }

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
