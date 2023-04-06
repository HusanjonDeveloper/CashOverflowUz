//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;

namespace CashOverflowUz.Brokers.DateTimes
{
	public class DateTimeBroker : IDateTimeBroker
	{
		public DateTimeOffset GetCurrentDateTimeOffset() =>
			DateTimeOffset.UtcNow;

	}
}
